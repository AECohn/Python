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

namespace UserModule_PYTHON_TEST
{
    public class UserModuleClass_PYTHON_TEST : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        Crestron.Logos.SplusObjects.DigitalInput TEST;
        CrestronString G_ID;
        Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule PMODULE;
        public override object FunctionMain (  object __obj__ ) 
            { 
            try
            {
                SplusExecutionContext __context__ = SplusFunctionMainStartCode();
                
                __context__.SourceCodeLine = 15;
                G_ID  .UpdateValue ( PythonAdapterUtils . NewGuid  ) ; 
                __context__.SourceCodeLine = 16;
                // RegisterDelegate( PythonInterface , DATARECEIVED , DATARECEIVEDHANDLER ) 
                PythonInterface .DataReceived  = DATARECEIVEDHANDLER; ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            return __obj__;
            }
            
        object TEST_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                ushort SUCCESS = 0;
                
                
                __context__.SourceCodeLine = 25;
                SUCCESS = (ushort) ( PythonInterface.Run( G_ID .ToString() , "/User/pythonscript.py" , PMODULE ) ) ; 
                __context__.SourceCodeLine = 26;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SUCCESS == 0))  ) ) 
                    { 
                    __context__.SourceCodeLine = 28;
                    PMODULE . SendData ( "{\"splus\":\"This data sent from S+ Run_\"}") ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    public void DATARECEIVEDHANDLER ( Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule SENDER , Crestron.SimplSharp.Python.SimplPlusAdapter.PythonAdapterDataReceivedEventArgs E ) 
        { 
        try
        {
            SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
            
            
            
        }
        finally { ObjectFinallyHandler(); }
        }
        
    
    public override void LogosSplusInitialize()
    {
        SocketInfo __socketinfo__ = new SocketInfo( 1, this );
        InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
        _SplusNVRAM = new SplusNVRAM( this );
        G_ID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 65534, this );
        
        TEST = new Crestron.Logos.SplusObjects.DigitalInput( TEST__DigitalInput__, this );
        m_DigitalInputList.Add( TEST__DigitalInput__, TEST );
        
        
        TEST.OnDigitalPush.Add( new InputChangeHandlerWrapper( TEST_OnPush_0, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        PMODULE  = new Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule();
        
        
    }
    
    public UserModuleClass_PYTHON_TEST ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint TEST__DigitalInput__ = 0;
    
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
