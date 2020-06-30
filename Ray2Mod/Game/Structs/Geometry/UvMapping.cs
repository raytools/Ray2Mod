using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Geometry
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UvMapping
    {
        public short mapping_0;
        public short mapping_1;
        public short mapping_2;
    }
}