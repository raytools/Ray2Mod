using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.States;
using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct StateTransitionListElement
        {
            public StateTransitionListElement* Next;
            public StateTransitionListElement* Previous;
            public StateTransitionListElement* Header;
            public StateTransition Element;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct StateTransitionList
        {
            public StateTransitionListElement* Head;
            public StateTransitionListElement* Tail;
            public int Count;

            public unsafe StateTransition[] Read()
            {
                StateTransition[] results = new StateTransition[Count];

                StateTransitionListElement* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (Next == null)
                    {
                        break;
                    }

                    StateTransitionListElement* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = LinkedListElement->Element;
                }

                return results;
            }

            public void Write(StateTransition[] items)
            {
                StateTransitionListElement*[] elements = new StateTransitionListElement*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++)
                {
                    StateTransitionListElement* NewElement = new StateTransitionListElement()
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

            public void Add(StateTransition item)
            {
                var items = Read();
                List<StateTransition> pointerList = new List<StateTransition>(items);
                pointerList.Add(item);
                Write(pointerList.ToArray());
            }

            public void Remove(StateTransition item)
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