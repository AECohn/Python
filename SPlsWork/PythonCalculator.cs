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

namespace UserModule_PYTHONCALCULATOR
{
    public class UserModuleClass_PYTHONCALCULATOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        Crestron.Logos.SplusObjects.DigitalInput DIVIDE;
        Crestron.Logos.SplusObjects.DigitalInput MULTIPLY;
        Crestron.Logos.SplusObjects.DigitalInput ADD;
        Crestron.Logos.SplusObjects.DigitalInput SUBTRACT;
        InOutArray<Crestron.Logos.SplusObjects.StringInput> VALUES;
        Crestron.Logos.SplusObjects.StringOutput FROM_PYTHON;
        CrestronString MODE;
        Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule PYTHONCALC;
        CrestronString G_ID;
        public void DATARECEIVEDHANDLER ( Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule SENDER , Crestron.SimplSharp.Python.SimplPlusAdapter.PythonAdapterDataReceivedEventArgs E ) 
            { 
            try
            {
                SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
                
                __context__.SourceCodeLine = 18;
                FROM_PYTHON  .UpdateValue ( E . Data  ) ; 
                
                
            }
            finally { ObjectFinallyHandler(); }
            }
            
        private void SENDTOPYTHON (  SplusExecutionContext __context__, CrestronString MODE ) 
            { 
            ushort I = 0;
            
            Crestron.SimplPlus.Utilities.StringList ARGS;
            ARGS  = new Crestron.SimplPlus.Utilities.StringList();
            
            
            __context__.SourceCodeLine = 27;
            ARGS . Add ( MODE .ToString()) ; 
            __context__.SourceCodeLine = 29;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)2; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 31;
                ARGS . Add ( VALUES[ I ] .ToString()) ; 
                __context__.SourceCodeLine = 29;
                } 
            
            __context__.SourceCodeLine = 35;
             PythonInterface.RunWithArgs(  G_ID .ToString() , "/user/PythonCalc.py" , PYTHONCALC , ARGS )  ;  
 
            
            }
            
        object DIVIDE_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 44;
                SENDTOPYTHON (  __context__ , "Divide") ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object MULTIPLY_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 50;
            SENDTOPYTHON (  __context__ , "Multiply") ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object ADD_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 57;
        SENDTOPYTHON (  __context__ , "Add") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SUBTRACT_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 64;
        SENDTOPYTHON (  __context__ , "Subtract") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

private void REGISTERDELEGATES (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 73;
    // RegisterDelegate( PythonInterface , DATARECEIVED , DATARECEIVEDHANDLER ) 
    PythonInterface .DataReceived  = DATARECEIVEDHANDLER; ; 
    
    }
    
public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 78;
        G_ID  .UpdateValue ( PythonAdapterUtils . NewGuid  ) ; 
        __context__.SourceCodeLine = 80;
        REGISTERDELEGATES (  __context__  ) ; 
        __context__.SourceCodeLine = 81;
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
    MODE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 64, this );
    G_ID  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 250, this );
    
    DIVIDE = new Crestron.Logos.SplusObjects.DigitalInput( DIVIDE__DigitalInput__, this );
    m_DigitalInputList.Add( DIVIDE__DigitalInput__, DIVIDE );
    
    MULTIPLY = new Crestron.Logos.SplusObjects.DigitalInput( MULTIPLY__DigitalInput__, this );
    m_DigitalInputList.Add( MULTIPLY__DigitalInput__, MULTIPLY );
    
    ADD = new Crestron.Logos.SplusObjects.DigitalInput( ADD__DigitalInput__, this );
    m_DigitalInputList.Add( ADD__DigitalInput__, ADD );
    
    SUBTRACT = new Crestron.Logos.SplusObjects.DigitalInput( SUBTRACT__DigitalInput__, this );
    m_DigitalInputList.Add( SUBTRACT__DigitalInput__, SUBTRACT );
    
    VALUES = new InOutArray<StringInput>( 2, this );
    for( uint i = 0; i < 2; i++ )
    {
        VALUES[i+1] = new Crestron.Logos.SplusObjects.StringInput( VALUES__AnalogSerialInput__ + i, VALUES__AnalogSerialInput__, 256, this );
        m_StringInputList.Add( VALUES__AnalogSerialInput__ + i, VALUES[i+1] );
    }
    
    FROM_PYTHON = new Crestron.Logos.SplusObjects.StringOutput( FROM_PYTHON__AnalogSerialOutput__, this );
    m_StringOutputList.Add( FROM_PYTHON__AnalogSerialOutput__, FROM_PYTHON );
    
    
    DIVIDE.OnDigitalPush.Add( new InputChangeHandlerWrapper( DIVIDE_OnPush_0, false ) );
    MULTIPLY.OnDigitalPush.Add( new InputChangeHandlerWrapper( MULTIPLY_OnPush_1, false ) );
    ADD.OnDigitalPush.Add( new InputChangeHandlerWrapper( ADD_OnPush_2, false ) );
    SUBTRACT.OnDigitalPush.Add( new InputChangeHandlerWrapper( SUBTRACT_OnPush_3, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    PYTHONCALC  = new Crestron.SimplSharp.Python.SimplPlusAdapter.PythonModule();
    
    
}

public UserModuleClass_PYTHONCALCULATOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint DIVIDE__DigitalInput__ = 0;
const uint MULTIPLY__DigitalInput__ = 1;
const uint ADD__DigitalInput__ = 2;
const uint SUBTRACT__DigitalInput__ = 3;
const uint VALUES__AnalogSerialInput__ = 0;
const uint FROM_PYTHON__AnalogSerialOutput__ = 0;

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
