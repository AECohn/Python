using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;
using Crestron.SimplPlus.Utilities;
using Crestron.SimplSharp.Python.SimplPlusAdapter;
using Crestron.SimplSharp.Python;

namespace UserModule_PYTHONEXAMPLE_2
{
    public class UserModuleClass_PYTHONEXAMPLE_2 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput RUN;
        Crestron.Logos.SplusObjects.DigitalInput RUNWITHARGS;
        Crestron.Logos.SplusObjects.DigitalInput TEST;
        Crestron.Logos.SplusObjects.DigitalOutput SUPPORTED;
        Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule [] G_PYTHONMODULE;
        Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule PMODULE;
        CrestronString G_ID;
        ushort G_COUNT = 0;
        ushort G_LASTRUNMODE = 0;
        ushort [] G_EXPECTEDARGCOUNT;
        private void PRINTMODULE (  SplusExecutionContext __context__, CrestronString HEADER , Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule MODULE ) 
            { 
            ushort I = 0;
            
            ushort COUNT = 0;
            
            
            __context__.SourceCodeLine = 50;
            Print( "{0} PythonModule[{1}]:\r\n", HEADER , G_ID ) ; 
            __context__.SourceCodeLine = 51;
            Print( "{0} UniqueIdentifier = {1}\r\n", HEADER , MODULE . UniqueIdentifier ) ; 
            __context__.SourceCodeLine = 53;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (MODULE.ArgumentCount == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 55;
                Print( "{0} Arguments=empty\r\n", HEADER ) ; 
                } 
            
            __context__.SourceCodeLine = 57;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( I < MODULE.ArgumentCount ))  ) ) 
                { 
                __context__.SourceCodeLine = 59;
                Print( "{0} Arguments[{1:d}] = {2}\r\n", HEADER , (short)I, MODULE . Arguments [ I ] ) ; 
                __context__.SourceCodeLine = 60;
                I = (ushort) ( (I + 1) ) ; 
                __context__.SourceCodeLine = 57;
                } 
            
            __context__.SourceCodeLine = 63;
            Print( "{0} File = {1}\r\n", HEADER , MODULE . File ) ; 
            __context__.SourceCodeLine = 64;
            Print( "{0} State = {1}\r\n", HEADER , MODULE . State ) ; 
            __context__.SourceCodeLine = 65;
            Print( "{0} StateInformation = {1}\r\n", HEADER , MODULE . StateInformation ) ; 
            
            }
            
        public void DATARECEIVEDHANDLER ( Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule SENDER , Crestron.SimplSharp.Python.SimplPlusAdapter.PythonAdapterDataReceivedEventArgs E ) 
            { 
            CrestronString S;
            S  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
            
            try
            {
                SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
                
                __context__.SourceCodeLine = 76;
                S  =  ( "{\"splus\":\"This data sent from S+ DataReceivedHandler\"}"  )  .ToString() ; 
                __context__.SourceCodeLine = 78;
                PRINTMODULE (  __context__ , "DataReceivedHandler:", SENDER) ; 
                __context__.SourceCodeLine = 80;
                Print( "e.Data = {0}\r\n", E . Data ) ; 
                __context__.SourceCodeLine = 83;
                SENDER . SendData ( S .ToString()) ; 
                
                
            }
            finally { ObjectFinallyHandler(); }
            }
            
        private void RUN_ (  SplusExecutionContext __context__, ushort MODE ) 
            { 
            Crestron.SimplPlus.Utilities.StringList ARGS;
            ARGS  = new Crestron.SimplPlus.Utilities.StringList();
            
            CrestronString DATA;
            DATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
            
            ushort RESULT = 0;
            
            
            __context__.SourceCodeLine = 95;
            DATA  .UpdateValue ( "{\"splus\":\"This data sent from S+ Run_\"}"  ) ; 
            __context__.SourceCodeLine = 98;
            G_ID  .UpdateValue ( PythonAdapterUtils . NewGuid  ) ; 
            __context__.SourceCodeLine = 101;
            ARGS . Add ( "arg1") ; 
            __context__.SourceCodeLine = 102;
            ARGS . Add ( "arg2") ; 
            __context__.SourceCodeLine = 104;
            
                {
                int __SPLS_TMPVAR__SWTCH_1__ = ((int)MODE);
                
                    { 
                    if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 0) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 111;
                        RESULT = (ushort) ( PythonInterface.Run( G_ID .ToString() , "/user/simpltest.py" , PMODULE ) ) ; 
                        } 
                    
                    else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 1) ) ) ) 
                        { 
                        __context__.SourceCodeLine = 116;
                        RESULT = (ushort) ( PythonInterface.RunWithArgs( G_ID .ToString() , "/user/simpltest.py" , G_PYTHONMODULE[ MODE ] , ARGS ) ) ; 
                        } 
                    
                    } 
                    
                }
                
            
            __context__.SourceCodeLine = 120;
            G_LASTRUNMODE = (ushort) ( MODE ) ; 
            __context__.SourceCodeLine = 122;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (RESULT == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 124;
                PRINTMODULE (  __context__ , "Run_", G_PYTHONMODULE[ MODE ]) ; 
                __context__.SourceCodeLine = 127;
                PMODULE . SendData ( DATA .ToString()) ; 
                __context__.SourceCodeLine = 129;
                 PythonInterface.SendData(  G_ID .ToString() ,  DATA .ToString() )  ;  
 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 134;
                Print( "FAIL: Run() returned {0:d}\r\n", (int)RESULT) ; 
                } 
            
            
            }
            
        private void MODULETEST (  SplusExecutionContext __context__, Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule MODULE ) 
            { 
            ushort RESULT = 0;
            
            
            __context__.SourceCodeLine = 145;
            G_EXPECTEDARGCOUNT [ 0] = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 146;
            G_EXPECTEDARGCOUNT [ 1] = (ushort) ( 2 ) ; 
            __context__.SourceCodeLine = 148;
            PRINTMODULE (  __context__ , "ModuleTest", MODULE) ; 
            __context__.SourceCodeLine = 151;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (G_EXPECTEDARGCOUNT[ G_LASTRUNMODE ] == MODULE.ArgumentCount))  ) ) 
                { 
                __context__.SourceCodeLine = 151;
                Print( "PASS: module.ArgumentCount={0:d}\r\n", (uint)MODULE.ArgumentCount) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 152;
                Print( "FAIL: module.ArgumentCount={0:d} expecting {1:d}\r\n", (uint)MODULE.ArgumentCount, (short)G_EXPECTEDARGCOUNT[ G_LASTRUNMODE ]) ; 
                } 
            
            __context__.SourceCodeLine = 154;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MODULE.ArgumentCount > 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 157;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ("arg1" == MODULE.Arguments[ 0 ]))  ) ) 
                    { 
                    __context__.SourceCodeLine = 157;
                    Print( "PASS: module.Arguments[0]={0}\r\n", MODULE . Arguments [ 0 ] ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 158;
                    Print( "FAIL: module.Arguments[0]={0} expecting {1}\r\n", MODULE . Arguments [ 0 ] , "arg1" ) ; 
                    } 
                
                __context__.SourceCodeLine = 160;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ("arg2" == MODULE.Arguments[ 1 ]))  ) ) 
                    {
                    __context__.SourceCodeLine = 160;
                    Print( "PASS: module.Arguments[1]={0}\r\n", MODULE . Arguments [ 1 ] ) ; 
                    }
                
                else 
                    {
                    __context__.SourceCodeLine = 161;
                    Print( "FAIL: module.Arguments[1]={0} expecting {1}\r\n", MODULE . Arguments [ 1 ] , "arg2" ) ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 165;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ("/user/simpltest.py" == MODULE.File))  ) ) 
                {
                __context__.SourceCodeLine = 165;
                Print( "PASS: module.File={0}\r\n", "/user/simpltest.py" ) ; 
                }
            
            else 
                {
                __context__.SourceCodeLine = 166;
                Print( "FAIL: module.File={0} expecting {1}\r\n", MODULE . File , "/user/simpltest.py" ) ; 
                }
            
            __context__.SourceCodeLine = 169;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ("Running" == MODULE.State))  ) ) 
                { 
                __context__.SourceCodeLine = 171;
                RESULT = (ushort) ( MODULE.Restart() ) ; 
                __context__.SourceCodeLine = 172;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (RESULT == -1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 172;
                    Print( "PASS: module.Restart()={0:d}\r\n", (short)RESULT) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 173;
                    Print( "FAIL: module.Restart()={0:d} expecting {1:d}\r\n", (short)RESULT, (short)-1) ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 175;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ("Stopped" == MODULE.State))  ) ) 
                { 
                __context__.SourceCodeLine = 177;
                RESULT = (ushort) ( MODULE.Restart() ) ; 
                __context__.SourceCodeLine = 178;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (RESULT == 0))  ) ) 
                    { 
                    __context__.SourceCodeLine = 178;
                    Print( "PASS: module.Restart()={0:d}\r\n", (short)RESULT) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 179;
                    Print( "FAIL: module.Restart()={0:d} expecting {1:d}\r\n", (short)RESULT, (short)0) ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 182;
            MODULE . SendData ( "{\"json\":\"This data sent from S+ to python module reference\"}") ; 
            
            }
            
        private void METHODTEST (  SplusExecutionContext __context__ ) 
            { 
            CrestronString DATA;
            DATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
            
            CrestronString RESPONSE;
            RESPONSE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
            
            
            __context__.SourceCodeLine = 193;
            MakeString ( DATA , "{{\"splus\":\"Sent from S+ this is a data store test {0:d}\"}}", (short)G_COUNT) ; 
            __context__.SourceCodeLine = 196;
            Print( "\r\nPythonInterface.ClearData({0})={1:d}\r\n", G_ID , (short)PythonInterface.ClearData( G_ID .ToString() )) ; 
            __context__.SourceCodeLine = 199;
            Print( "\r\nPythonInterface.SendData({0})={1:d}\r\n", G_ID , (short)PythonInterface.SendData( G_ID .ToString() , DATA .ToString() )) ; 
            __context__.SourceCodeLine = 202;
            RESPONSE  .UpdateValue ( PythonInterface . GetData (  G_ID  .ToString() )  ) ; 
            __context__.SourceCodeLine = 203;
            Print( "PythonInterface.GetData({0})={1}\r\n", G_ID , RESPONSE ) ; 
            __context__.SourceCodeLine = 206;
            Print( "\r\nPythonInterface.SendDataStored({0})={1:d}\r\n", G_ID , (short)PythonInterface.SendDataStored( G_ID .ToString() , DATA .ToString() )) ; 
            __context__.SourceCodeLine = 209;
            RESPONSE  .UpdateValue ( PythonInterface . GetData (  G_ID  .ToString() )  ) ; 
            __context__.SourceCodeLine = 210;
            Print( "PythonInterface.GetData({0})={1}\r\n", G_ID , RESPONSE ) ; 
            
            }
            
        private void TEST_ (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 215;
            G_COUNT = (ushort) ( (G_COUNT + 1) ) ; 
            __context__.SourceCodeLine = 218;
            MODULETEST (  __context__ , G_PYTHONMODULE[ G_LASTRUNMODE ]) ; 
            __context__.SourceCodeLine = 219;
            METHODTEST (  __context__  ) ; 
            
            }
            
        object RUN_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 224;
                RUN_ (  __context__ , (ushort)( 0 )) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object RUNWITHARGS_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 229;
            RUN_ (  __context__ , (ushort)( 1 )) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object TEST_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 234;
        TEST_ (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

private void REGISTERDELEGATES (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 240;
    // RegisterDelegate( PythonInterface , DATARECEIVED , DATARECEIVEDHANDLER ) 
    PythonInterface .DataReceived  = DATARECEIVEDHANDLER; ; 
    
    }
    
public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 245;
        SUPPORTED  .Value = (ushort) ( PythonInterface.Supported ) ; 
        __context__.SourceCodeLine = 247;
        REGISTERDELEGATES (  __context__  ) ; 
        __context__.SourceCodeLine = 249;
        WaitForInitializationComplete ( ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    G_EXPECTEDARGCOUNT  = new ushort[ 2 ];
    G_ID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
    
    RUN = new Crestron.Logos.SplusObjects.DigitalInput( RUN__DigitalInput__, this );
    m_DigitalInputList.Add( RUN__DigitalInput__, RUN );
    
    RUNWITHARGS = new Crestron.Logos.SplusObjects.DigitalInput( RUNWITHARGS__DigitalInput__, this );
    m_DigitalInputList.Add( RUNWITHARGS__DigitalInput__, RUNWITHARGS );
    
    TEST = new Crestron.Logos.SplusObjects.DigitalInput( TEST__DigitalInput__, this );
    m_DigitalInputList.Add( TEST__DigitalInput__, TEST );
    
    SUPPORTED = new Crestron.Logos.SplusObjects.DigitalOutput( SUPPORTED__DigitalOutput__, this );
    m_DigitalOutputList.Add( SUPPORTED__DigitalOutput__, SUPPORTED );
    
    
    RUN.OnDigitalPush.Add( new InputChangeHandlerWrapper( RUN_OnPush_0, false ) );
    RUNWITHARGS.OnDigitalPush.Add( new InputChangeHandlerWrapper( RUNWITHARGS_OnPush_1, false ) );
    TEST.OnDigitalPush.Add( new InputChangeHandlerWrapper( TEST_OnPush_2, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    PMODULE  = new Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule();
    G_PYTHONMODULE  = new Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule[ 2 ];
    for( uint i = 0; i < 2; i++ )
    {
        G_PYTHONMODULE [i] = new Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule();
        
    }
    
    
}

public UserModuleClass_PYTHONEXAMPLE_2 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint RUN__DigitalInput__ = 0;
const uint RUNWITHARGS__DigitalInput__ = 1;
const uint TEST__DigitalInput__ = 2;
const uint SUPPORTED__DigitalOutput__ = 0;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
