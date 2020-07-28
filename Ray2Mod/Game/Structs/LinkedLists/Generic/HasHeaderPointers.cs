using System.Runtime.InteropServices;

using Ray2Mod.Utils;

namespace Ray2Mod.Game.Structs.LinkedLists
{

    public abstract unsafe partial class LinkedList
    {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_HHP<T> where T : unmanaged
        {
            public ListElement_HHP<T>* Next;
            public ListElement_HHP<T>* Previous;
            public ListElement_HHP<T>* Header;
            public T Element;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HasHeaderPointers<T> where T : unmanaged
        {
            public ListElement_HHP<T>* Head;
            public ListElement_HHP<T>* Tail;
            public int Count;

            public unsafe ListElement_HHP<T>*[] Read()
            {
                ListElement_HHP<T>*[] results = new ListElement_HHP<T>*[Count];

                ListElement_HHP<T>* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (Next == null)
                    {
                        break;
                    }

                    ListElement_HHP<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = LinkedListElement;
                }

                return results;
            }

            public void Write(T[] items)
            {
                ListElement_HHP<T>*[] elements = new ListElement_HHP<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++)
                {
                    ListElement_HHP<T>* NewElement = new ListElement_HHP<T>()
                    {
                        Element = items[i],
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

            public void Write(ListElement_HHP<T>*[] items)
            {
                Count = items.Length;

                for (int i = 0; i < Count; i++)
                {

                    if (i == 0)
                    {
                        Head = items[i];
                    }
                    else if (i == Count - 1)
                    {
                        Tail = items[i];
                    }
                }

                // Set Next and Previous
                for (int i = 0; i < Count; i++)
                {
                    if (i > 0) { items[i]->Previous = items[i - 1]; }
                    if (i < Count - 1) { items[i]->Next = items[i + 1]; }
                }
            }

            /// <summary>
            /// Adds a new item to the list
            /// </summary>
            /// <param name="item">The item to add</param>
            /// <returns>A pointer to the newly created list element</returns>
            public ListElement_HHP<T>* Add(T item)
            {
                ListElement_HHP<T>*[] items = Read();

                ListElement_HHP<T>* newItem = new ListElement_HHP<T>()
                {
                    Next = null,
                    Previous = items.Length > 0 ? items[items.Length - 1] : null,
                    Header = Head,
                    Element = item
                }.ToUnmanaged();

                if (items.Length > 0)
                {
                    items[items.Length - 1]->Next = newItem;
                    Tail = newItem;
                }
                else
                {
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
            public bool Remove(ListElement_HHP<T>* item)
            {
                ListElement_HHP<T>*[] items = Read();

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == item)
                    {
                        if (i + 1 < items.Length)
                        {
                            items[i + 1]->Previous = i - 1 >= 0 ? items[i - 1] : null;
                        }

                        if (i - 1 >= 0)
                        {
                            items[i - 1]->Next = i + 1 < items.Length ? items[i + 1] : null;
                        }

                        if (i == 0)
                        {
                            Head = Count > 1 ? items[1] : null;
                        }
                        else if (i == Count - 1)
                        {
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
