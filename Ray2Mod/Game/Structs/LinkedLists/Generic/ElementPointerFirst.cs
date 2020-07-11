using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_EPF<T> where T : unmanaged {
            public T Element;
            public ListElement_EPF<T>* Next;
            public ListElement_EPF<T>* Previous;
            // No Header Pointers
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ElementPointerFirst<T> where T : unmanaged {
            public ListElement_EPF<T>* Head;
            public ListElement_EPF<T>* Tail;
            public int Count;

            public unsafe T[] Read() {
                T[] results = new T[Count];

                ListElement_EPF<T>* Next = Head;

                for (int i = 0; i < Count; i++) {
                    if (Next == null) {
                        break;
                    }

                    ListElement_EPF<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = (T)LinkedListElement->Element;
                }

                return results;
            }

            public void Write(T[] items) {
                ListElement_EPF<T>*[] elements = new ListElement_EPF<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++) {
                    ListElement_EPF<T>* NewElement = new ListElement_EPF<T>() {
                        Element = items[i],
                        Previous = null,
                        Next = null,
                    }.ToUnmanaged();

                    if (i == 0) {
                        Head = NewElement;
                    }
                    else if (i == Count - 1) {
                        Tail = NewElement;
                    }

                    // No Header Pointers

                    elements[i] = NewElement;
                }

                // Set Next and Previous
                for (int i = 0; i < Count; i++) {
                    if (i > 0) { elements[i]->Previous = elements[i - 1]; }
                    if (i < Count - 1) { elements[i]->Next = elements[i + 1]; }
                }
            }

            public void Add(T item) {
                var items = new List<T>(Read());
                items.Add(item);
                Write(items.ToArray());
            }

            public void Remove(T item) {
                var items = new List<T>(Read());
                items.Remove(item);
                Write(items.ToArray());
            }
        }
    }
}