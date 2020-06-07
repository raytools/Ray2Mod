using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SuperObjectList {
            public SuperObject * Head;
            public SuperObject * Tail;
            public int Count;

            public unsafe SuperObject*[] Read()
            {
                SuperObject*[] results = new SuperObject*[Count];

                SuperObject* Next = Head;

                for (int i = 0; i < Count; i++) {
                    SuperObject* Element = Next;
                    Next = Next->nextBrother;
                    // Previous pointer is ignored

                    results[i] = (SuperObject*)(Element);
                }

                return results;
            }
        }
    }
}
