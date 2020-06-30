using Ray2Mod.Game.Structs.EngineObject;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NeighborSectorList
        {
            public NeighborSector* Head;
            public NeighborSector* Tail;
            public int Count;

            public unsafe NeighborSector*[] Read()
            {
                NeighborSector*[] results = new NeighborSector*[Count];

                NeighborSector* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    NeighborSector* Element = Next;
                    Next = Next->off_next;
                    // Previous pointer is ignored

                    results[i] = (NeighborSector*)(Element);
                }

                return results;
            }
        }
    }
}