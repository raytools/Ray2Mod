using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NeighborSector
    {

        public ushort short0;
        public ushort short2;
        public Sector* sector;

        public NeighborSector* off_next;
        public NeighborSector* off_previous;
    }
}