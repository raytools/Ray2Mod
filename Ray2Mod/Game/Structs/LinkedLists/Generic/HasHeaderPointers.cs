﻿using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists {

    public abstract unsafe partial class LinkedList {

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ListElement_HHP<T> where T : unmanaged {
            public ListElement_HHP<T>* Next;
            public ListElement_HHP<T>* Previous;
            public ListElement_HHP<T>* Header;
            public T Element;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HasHeaderPointers<T> where T : unmanaged {
            public ListElement_HHP<T>* Head;
            public ListElement_HHP<T>* Tail;
            public int Count;

            public unsafe T[] Read() {
                T[] results = new T[Count];

                ListElement_HHP<T>* Next = Head;

                for (int i = 0; i < Count; i++) {
                    if (Next == null) {
                        break;
                    }

                    ListElement_HHP<T>* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = (T)LinkedListElement->Element;
                }

                return results;
            }

            public void Write(T[] items) {
                ListElement_HHP<T>*[] elements = new ListElement_HHP<T>*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++) {
                    ListElement_HHP<T>* NewElement = new ListElement_HHP<T>() {
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

                    NewElement->Header = Head;

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