using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{

    public abstract unsafe partial class LinkedList
    {

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
