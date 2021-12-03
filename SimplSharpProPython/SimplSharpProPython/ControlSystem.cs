using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.Diagnostics;		    	// For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;         	// For Generic Device Support
using Crestron.SimplSharpPro.UI;
using Crestron.SimplSharp.Python;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PythonDemo
{
    public class ControlSystem : CrestronControlSystem
    {
        private Xpanel xpanel;
        private PythonModuleInstance webscrapingModule;
        public string File_Path = string.Empty;

        /// <summary>
        /// ControlSystem Constructor. Starting point for the SIMPL#Pro program.
        /// </summary>
        public ControlSystem() : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                xpanel = new Xpanel(0x05, this);

                xpanel.SigChange += new SigEventHandler(Xpanel_Sigchange);
                xpanel.Register();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        /// <summary>
        /// InitializeSystem - this method gets called after the constructor 
        /// if it doesn't exit in time, the SIMPL#Pro program will exit.
        /// </summary>
        public override void InitializeSystem()
        {
            try
            {
                CrestronConsole.AddNewConsoleCommand(StartWebScrap, "StartWebScrap", "StartWebScrap <url>", ConsoleAccessLevelEnum.AccessAdministrator);
                CrestronConsole.AddNewConsoleCommand(setup, "setupPy", "Setup the Python dependency path.", ConsoleAccessLevelEnum.AccessAdministrator);
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        private void setup(string cmd)
        {
            SimplSharpPythonInterface.Run("uid_setup", "setup.py", SetupCallback, new string[] { "" });
        }

        private void StartWebScrap(string cmdParameters)
        {
            ErrorLog.Notice("Web Scraping process started for {0}\r\n", cmdParameters);
            webscrapingModule = SimplSharpPythonInterface.Run("uid_web", "web_scraping_demo.py", webloopCallback, cmdParameters);
        }
        private void Xpanel_Sigchange(BasicTriList currentDevice, SigEventArgs args)
        {

            switch (args.Sig.Type)
            {
                case eSigType.Bool:
                    {
                        switch (args.Sig.Number)
                        {
                            //Run Web scrapping module
                            case 1:
                                {
                                    if (args.Sig.BoolValue)
                                    {
                                        if (!xpanel.BooleanInput[1].BoolValue)
                                        {
                                            xpanel.StringInput[3].StringValue = "";
                                            xpanel.StringInput[2].StringValue = "";
                                            string[] arguments = xpanel.StringOutput[1].StringValue.Split(' ');
                                            SimplSharpPythonInterface.SendData("uid_web", "{\"URL\":\"" + arguments[0] + "\"}", JsonDb.eSaveType.DataStored);
                                            xpanel.BooleanInput[1].BoolValue = false;
                                        }
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (args.Sig.BoolValue)
                                    {
                                        // If not running, start it
                                        if (!xpanel.BooleanInput[2].BoolValue)
                                        {
                                            webscrapingModule = SimplSharpPythonInterface.Run("uid_web", "web_scraping_demo.py", webloopCallback, new string[] { "" });
                                            SimplSharpPythonInterface.ClearData("uid_web");
                                            xpanel.BooleanInput[2].BoolValue = true;
                                        }
                                        // If running, send stop
                                        else
                                        {
                                            if (webscrapingModule != null)
                                            {
                                                SimplSharpPythonInterface.SendData("uid_web", "{\"Running\":\"False\"}", JsonDb.eSaveType.DataStored);

                                                webscrapingModule = null;
                                                xpanel.BooleanInput[2].BoolValue = false;
                                                xpanel.StringInput[3].StringValue = "";
                                                xpanel.StringInput[2].StringValue = "";
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    }
            }
        }

        private void webloopCallback(object sender, PythonDataReceivedEventArgs e)
        {
            var fileDetails = JObject.Parse(e.Data);

            string fileStatus = (string)fileDetails["file_status"];
            string filePath = (string)fileDetails["file_path"];

            if (!string.IsNullOrEmpty(filePath))
                File_Path = filePath;

            xpanel.StringInput[3].StringValue = fileStatus;

            if (fileStatus == "Completed")
                if (string.IsNullOrEmpty(filePath))
                {
                    CrestronConsole.PrintLine("File Path: {0}", File_Path);
                    xpanel.StringInput[2].StringValue = File_Path;
                }

            CrestronConsole.PrintLine("Status: {0}", fileStatus);
        }

        private void SetupCallback(object sender, PythonDataReceivedEventArgs e)
        {
        }
    }
}