namespace Crestron.SimplPlus.Utilities;
        // class declarations
         class StringList;
         class PythonAdapterUtils;
     class StringList 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION ElementAt ( SIGNED_LONG_INTEGER index );
        STRING_FUNCTION get_Item ( SIGNED_LONG_INTEGER index );
        FUNCTION set_Item ( SIGNED_LONG_INTEGER index , STRING value );
        FUNCTION Add ( STRING item );
        SIGNED_LONG_INTEGER_FUNCTION BinarySearch ( STRING item );
        FUNCTION Clear ();
        SIGNED_LONG_INTEGER_FUNCTION IndexOf ( STRING item );
        FUNCTION Insert ( SIGNED_LONG_INTEGER index , STRING item );
        SIGNED_LONG_INTEGER_FUNCTION LastIndexOf ( STRING item );
        FUNCTION RemoveAt ( SIGNED_LONG_INTEGER index );
        FUNCTION RemoveRange ( SIGNED_LONG_INTEGER index , SIGNED_LONG_INTEGER count );
        FUNCTION Reverse ();
        FUNCTION Sort ();
        FUNCTION TrimExcess ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Elements[][];
        SIGNED_LONG_INTEGER Capacity;
        SIGNED_LONG_INTEGER Count;
    };

    static class PythonAdapterUtils 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING NewGuid[];
    };

namespace Crestron.SimplSharp.Python.SimplPlusAdapter;
        // class declarations
         class PythonAdapterDataReceivedEventArgs;
         class PythonErrorCodes;
         class PythonModule;
         class PythonInterface;
     class PythonAdapterDataReceivedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Data[];
    };

    static class PythonErrorCodes 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        static SIGNED_LONG_INTEGER SUCCESS;
        static SIGNED_LONG_INTEGER FAILURE;

        // class properties
    };

     class PythonModule 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION Initialize ( STRING uniqueIdentifier );
        SIGNED_LONG_INTEGER_FUNCTION Restart ();
        SIGNED_LONG_INTEGER_FUNCTION SendData ( STRING jsonObjectToSend );
        STRING_FUNCTION ToString ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING UniqueIdentifier[];
        SIGNED_LONG_INTEGER ArgumentCount;
        STRING Arguments[][];
        STRING File[];
        STRING State[];
        STRING StateInformation[];
    };

    static class PythonInterface 
    {
        // class delegates
        delegate FUNCTION PythonDataReceivedEventHandler ( PythonModule sender , PythonAdapterDataReceivedEventArgs e );

        // class events

        // class functions
        static SIGNED_LONG_INTEGER_FUNCTION Run ( STRING uniqueIdentifier , STRING modulePath , PythonModule pythonModule );
        static SIGNED_LONG_INTEGER_FUNCTION RunWithArgs ( STRING uniqueIdentifier , STRING modulePath , PythonModule pythonModule , StringList moduleArguments );
        static SIGNED_LONG_INTEGER_FUNCTION ClearData ( STRING uniqueIdentifier );
        static STRING_FUNCTION GetData ( STRING uniqueIdentifier );
        static SIGNED_LONG_INTEGER_FUNCTION SendData ( STRING uniqueIdentifier , STRING jsonObjectToSend );
        static SIGNED_LONG_INTEGER_FUNCTION SendDataStored ( STRING uniqueIdentifier , STRING jsonObjectToSend );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Supported;
        DelegateProperty PythonDataReceivedEventHandler DataReceived;
    };

