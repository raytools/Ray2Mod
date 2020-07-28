using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.SPO;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct NeighborGraphicSector
        {
            public short short_0;
            public short short_1;
            public SuperObject* sector;
            public NeighborGraphicSector* off_next;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NeighborGraphicSectorList
        {
            public NeighborGraphicSector* Head;
            public NeighborGraphicSector* Tail;
            public int Count;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct NeightborCollisionSector
        {
            public SuperObject* sector;
            public NeightborCollisionSector* off_next;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NeighborCollisionSectorList
        {
            public NeightborCollisionSector* Head;
            public NeightborCollisionSector* Tail;
            public int Count;
        }
    }
}