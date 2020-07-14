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

            public unsafe ListElement_EPF<T>*[] Read()
            {
                ListElement_EPF<T>*[] results = new ListElement_EPF<T>*[Count];

                ListElement_EPF<T>* Next = Head;

                for (int i = 0; i < Count; i++) {
                    if (Next == null) {
                        break;
                    }

                    ListElement_EPF<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = LinkedListElement;
                }

                return results;
            }

            public void Write(T[] items)
            {
                ListElement_EPF<T>*[] elements = new ListElement_EPF<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++) {
                    ListElement_EPF<T>* NewElement = new ListElement_EPF<T>()
                    {
                        Element = items[i],
                        Previous = null,
                        Next = null,
                    }.ToUnmanaged();

                    if (i == 0) {
                        Head = NewElement;
                    } else if (i == Count - 1) {
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

            public void Write(ListElement_EPF<T>*[] items)
            {
                Count = items.Length;

                for (int i = 0; i < Count; i++) {

                    if (i == 0) {
                        Head = items[i];
                    } else if (i == Count - 1) {
                        Tail = items[i];
                    }
                }

                // Set Next and Previous
                for (int i = 0; i < Count; i++) {
                    if (i > 0) { items[i]->Previous = items[i - 1]; }
                    if (i < Count - 1) { items[i]->Next = items[i + 1]; }
                }
            }

            /// <summary>
            /// Adds a new item to the list
            /// </summary>
            /// <param name="item">The item to add</param>
            /// <returns>A pointer to the newly created list element</returns>
            public ListElement_EPF<T>* Add(T item)
            {
                var items = Read();

                var newItem = new ListElement_EPF<T>()
                {
                    Next = null,
                    Previous = items[items.Length - 1],
                    // No Header
                    Element = item
                }.ToUnmanaged();

                items[items.Length - 1]->Next = newItem;

                Count++;

                return newItem;
            }

            /// <summary>
            /// Removes the first occurrence of an item from the list
            /// </summary>
            /// <param name="item">The item to remove</param>
            /// <returns>True if the item was found and removed, false otherwise</returns>
            public bool Remove(ListElement_EPF<T>* item)
            {
                var items = Read();

                for (int i = 0; i < items.Length; i++) {
                    if (items[i] == item) {
                        if (i + 1 < items.Length) {
                            items[i + 1]->Previous = i - 1 >= 0 ? items[i - 1] : null;
                        }

                        if (i - 1 >= 0) {
                            items[i - 1]->Next = i + 1 < items.Length ? items[i + 1] : null;
                        }

                        Count--;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}