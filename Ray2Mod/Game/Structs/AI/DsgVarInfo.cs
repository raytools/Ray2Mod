using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DsgVarInfo
    {
        public int offsetInBuffer;
        public DsgVarType type;
        public int saveType;
        public int initType;
    }
}