using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DsgVar {
        public void * dsgMemBuffer;
        public DsgVarInfo * dsgVarInfos;
        public int memBufferLength;

        public byte dsgVarInfosLength;
        public byte padding_D;
        public byte padding_E;
        public byte padding_F;
    }
}