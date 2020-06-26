using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{

    public abstract unsafe partial class LinkedList
    {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NoPreviousPointersForDouble_ElementPointerFirst_ReadAtPointer<T> where T : unmanaged
        {
            public int* Head;
            public int* Tail;
            public int Count;

            public unsafe T*[] Read()
            {
                T*[] results = new T*[Count];

                int* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    int* LinkedListElement = Next;

                    int* Element = (int*)(*LinkedListElement);
                    Next = (int*)(*(LinkedListElement + 1));
                    // No Previous Pointer

                    results[i] = (T*)((int)(Element));
                }

                return results;
            }
        }
    }
}
