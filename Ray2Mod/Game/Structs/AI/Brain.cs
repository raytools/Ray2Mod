using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    // Sizeof = 0xC
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Brain
    {
        public Mind* mind;
        public int field_0x4;
        public int field_0x8;
    }
}