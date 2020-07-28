using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Input
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct KeyWord
    {
        public int indexOrKeyCode;
        public int field_4;

        public int valueAsInt => (short)indexOrKeyCode;

        public EntryAction* indexAsPointer => (EntryAction*)indexOrKeyCode;

        public byte Index => (byte)(indexOrKeyCode & 0xFF);

        public Functions.FunctionType FunctionType
        {
            get
            {
                if (Index <= Enum.GetNames(typeof(Functions.FunctionType)).Length)
                {
                    return Functions.GetFunctionType(Index);
                }
                else
                {
                    return Functions.FunctionType.Unknown;
                }
            }
        }


    }
}