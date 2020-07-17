using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_NPP_EPF<T> where T : unmanaged {
            public T Element;
            public ListElement_NPP_EPF<T>* Next;
            // No Header
            // No Previous
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NoPreviousPointers_ElementPointerFirst<T> where T : unmanaged {
            public ListElement_NPP_EPF<T>* Head;
            public ListElement_NPP_EPF<T>* Tail;
            public int Count;

            public unsafe ListElement_NPP_EPF<T>*[] Read()
            {
                ListElement_NPP_EPF<T>*[] results = new ListElement_NPP_EPF<T>*[Count];

                ListElement_NPP_EPF<T>* Next = Head;

                for (int i = 0; i < Count; i++) {
                    if (Next == null) {
                        break;
                    }

                    ListElement_NPP_EPF<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = LinkedListElement;
                }

                return results;
            }

            public void Write(T[] items)
            {
                ListElement_NPP_EPF<T>*[] elements = new ListElement_NPP_EPF<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++) {
                    ListElement_NPP_EPF<T>* NewElement = new ListElement_NPP_EPF<T>()
                    {
                        Element = items[i],
                        // No Previous Pointers
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
                    // No Previous Pointers
                    if (i < Count - 1) { elements[i]->Next = elements[i + 1]; }
                }
            }

            public void Write(ListElement_NPP_EPF<T>*[] items)
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
                    // No Previous Pointers
                    if (i < Count - 1) { items[i]->Next = items[i + 1]; }
                }
            }

            /// <summary>
            /// Adds a new item to the list
            /// </summary>
            /// <param name="item">The item to add</param>
            /// <returns>A pointer to the newly created list element</returns>
            public ListElement_NPP_EPF<T>* Add(T item)
            {
                var items = Read();

                var newItem = new ListElement_NPP_EPF<T>()
                {
                    Next = null,
                    // No Previous Pointers
                    // No Header
                    Element = item
                }.ToUnmanaged();

                if (items.Length > 0) {
                    items[items.Length - 1]->Next = newItem;
                    Tail = newItem;
                } else {
                    Head = newItem;
                    Tail = newItem;
                }

                Count++;

                return newItem;
            }

            /// <summary>
            /// Removes the first occurrence of an item from the list
            /// </summary>
            /// <param name="item">The item to remove</param>
            /// <returns>True if the item was found and removed, false otherwise</returns>
            public bool Remove(ListElement_NPP_EPF<T>* item)
            {
                var items = Read();

                for (int i = 0; i < items.Length; i++) {
                    if (items[i] == item) {
                        if (i + 1 < items.Length) {
                            // No Previous Pointers
                        }

                        if (i - 1 >= 0) {
                            items[i - 1]->Next = i + 1 < items.Length ? items[i + 1] : null;
                        }

                        if (i == 0) {
                            Head = Count > 1 ? items[1] : null;
                        } else if (i == Count - 1) {
                            Tail = Count > 1 ? items[i - 1] : null;
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