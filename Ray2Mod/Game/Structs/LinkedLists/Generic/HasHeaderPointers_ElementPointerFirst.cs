using Ray2Mod.Components.Types;
using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_HHP_EPF
        {
            public int* Element;
            public ListElement_HHP_EPF* Next;
            public ListElement_HHP_EPF* Previous;
            public ListElement_HHP_EPF* Header;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HasHeaderPointers_ElementPointerFirst<T> where T : unmanaged
        {
            public ListElement_HHP_EPF* Head;
            public ListElement_HHP_EPF* Tail;
            public int Count;

            public unsafe T*[] Read()
            {
                T*[] results = new T*[Count];

                ListElement_HHP_EPF* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (Next == null)
                    {
                        break;
                    }

                    ListElement_HHP_EPF* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = (T*)LinkedListElement->Element;
                }

                return results;
            }

            public void Write(T*[] items)
            {
                ListElement_HHP_EPF*[] elements = new ListElement_HHP_EPF*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++)
                {
                    ListElement_HHP_EPF* NewElement = new ListElement_HHP_EPF()
                    {
                        Element = (int*)items[i],
                        Previous = null,
                        Next = null,
                    }.ToUnmanaged();

                    if (i == 0)
                    {
                        Head = NewElement;
                    }
                    else if (i == Count - 1)
                    {
                        Tail = NewElement;
                    }

                    NewElement->Header = Head;

                    elements[i] = NewElement;
                }

                // Set Next and Previous
                for (int i = 0; i < Count; i++)
                {
                    if (i > 0) { elements[i]->Previous = elements[i - 1]; }
                    if (i < Count - 1) { elements[i]->Next = elements[i + 1]; }
                }
            }

            public void Add(T* item)
            {
                var items = Pointer<T>.WrapPointerArray(Read());
                List<Pointer<T>> pointerList = new List<Pointer<T>>(items);
                pointerList.Add(item);
                Write(Pointer<T>.PointerListToArray(pointerList));
            }

            public void Remove(T* item)
            {
                var items = Pointer<T>.WrapPointerArray(Read());
                List<Pointer<T>> pointerList = new List<Pointer<T>>(items);
                pointerList.Remove(item);
                var newLength = pointerList.Count;
                Write(Pointer<T>.PointerListToArray(pointerList));
            }
        }
    }
}