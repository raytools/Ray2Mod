using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Input {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct KeyWord {
        public int indexOrKeyCode;
        public int field_4;

        public int valueAsInt
        {
            get
            {
                return (short)indexOrKeyCode;
            }
        }

        public EntryElement* indexAsPointer
        {
            get
            {
                return (EntryElement*)indexOrKeyCode;
            }
        }

        public byte Index
        {
            get { return (byte)(indexOrKeyCode & 0xFF); }
        }

        public InputFunctions.FunctionType FunctionType
        {
            get
            {
                if (Index <= Enum.GetNames(typeof(InputFunctions.FunctionType)).Length) {
                    return InputFunctions.GetFunctionType(Index);
                } else {
                    return InputFunctions.FunctionType.Unknown;
                }
            }
        }
        
        
    }
}