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

        public EntryAction* indexAsPointer
        {
            get
            {
                return (EntryAction*)indexOrKeyCode;
            }
        }

        public byte Index
        {
            get { return (byte)(indexOrKeyCode & 0xFF); }
        }

        public Functions.FunctionType FunctionType
        {
            get
            {
                if (Index <= Enum.GetNames(typeof(Functions.FunctionType)).Length) {
                    return Functions.GetFunctionType(Index);
                } else {
                    return Functions.FunctionType.Unknown;
                }
            }
        }
        
        
    }
}