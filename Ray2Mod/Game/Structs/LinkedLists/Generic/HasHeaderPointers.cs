using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HasHeaderPointers {
            public int* Head;
            public int* Tail;
            public int Count;

            public unsafe T*[] Read<T>() where T : unmanaged
            {
                T*[] results = new T*[Count];

                int* Next = Head;

                for (int i = 0; i < Count; i++) {
                    int* LinkedListElement = Next;
                    Next = (int*)(*LinkedListElement);
                    int* Previous = (int*)(*(LinkedListElement + 1));
                    int* Header = (int*)(*(LinkedListElement + 2));
                    int* Element = (int*)((LinkedListElement + 3)); // don't dereference pointer

                    results[i] = (T*)((int)(Element));
                }

                return results;
            }
        }

    }
}
