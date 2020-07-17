using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_HHP_EPF<T> where T : unmanaged {
            public T Element;
            public ListElement_HHP_EPF<T>* Next;
            public ListElement_HHP_EPF<T>* Previous;
            public ListElement_HHP_EPF<T>* Header;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HasHeaderPointers_ElementPointerFirst<T> where T : unmanaged {
            public ListElement_HHP_EPF<T>* Head;
            public ListElement_HHP_EPF<T>* Tail;
            public int Count;

            public unsafe ListElement_HHP_EPF<T>*[] Read()
            {
                ListElement_HHP_EPF<T>*[] results = new ListElement_HHP_EPF<T>*[Count];

                ListElement_HHP_EPF<T>* Next = Head;

                for (int i = 0; i < Count; i++) {
                    if (Next == null) {
                        break;
                    }

                    ListElement_HHP_EPF<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = LinkedListElement;
                }

                return results;
            }

            public void Write(T[] items)
            {
                ListElement_HHP_EPF<T>*[] elements = new ListElement_HHP_EPF<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++) {
                    ListElement_HHP_EPF<T>* NewElement = new ListElement_HHP_EPF<T>()
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

                    NewElement->Header = Head;

                    elements[i] = NewElement;
                }

                // Set Next and Previous
                for (int i = 0; i < Count; i++) {
                    if (i > 0) { elements[i]->Previous = elements[i - 1]; }
                    if (i < Count - 1) { elements[i]->Next = elements[i + 1]; }
                }
            }

            public void Write(ListElement_HHP_EPF<T>*[] items)
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
            public ListElement_HHP_EPF<T>* Add(T item)
            {
                var items = Read();

                var newItem = new ListElement_HHP_EPF<T>()
                {
                    Next = null,
                    Previous = items.Length > 0 ? items[items.Length - 1] : null,
                    Header = Head,
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
            public bool Remove(ListElement_HHP_EPF<T>* item)
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