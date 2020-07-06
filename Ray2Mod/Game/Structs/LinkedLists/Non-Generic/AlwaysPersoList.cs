using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.EngineObject;
using Ray2Mod.Utils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AlwaysPersoListElement
        {
            public AlwaysPersoListElement* Next;
            public AlwaysPersoListElement* Previous;
            public AlwaysPersoListElement* Header;
            /// <summary>
            /// Model ID is checked when allocating an always object
            /// </summary>
            public int modelID;
            public Perso* Element;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AlwaysPersoList
        {
            public AlwaysPersoListElement* Head;
            public AlwaysPersoListElement* Tail;
            public int Count;

            public unsafe Perso*[] Read()
            {
                Perso*[] results = new Perso*[Count];

                AlwaysPersoListElement* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (Next == null)
                    {
                        break;
                    }

                    AlwaysPersoListElement* LinkedListElement = Next;
                    Next = LinkedListElement->Next;

                    results[i] = (Perso*)LinkedListElement->Element;
                }

                return results;
            }

            public void Write(Perso*[] items)
            {
                AlwaysPersoListElement*[] elements = new AlwaysPersoListElement*[items.Length];

                Count = elements.Length;

                for (int i = 0; i < Count; i++)
                {
                    AlwaysPersoListElement* NewElement = new AlwaysPersoListElement()
                    {
                        modelID = items[i]->stdGamePtr->modelID,
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

            public void Add(Perso* item)
            {
                var items = Pointer<Perso>.WrapPointerArray(Read());
                List<Pointer<Perso>> pointerList = new List<Pointer<Perso>>(items);
                pointerList.Add(item);
                Write(Pointer<Perso>.PointerListToArray(pointerList));
            }

            public void Remove(Perso* item)
            {
                var items = Pointer<Perso>.WrapPointerArray(Read());
                List<Pointer<Perso>> pointerList = new List<Pointer<Perso>>(items);
                pointerList.Remove(item);
                var newLength = pointerList.Count;
                Write(Pointer<Perso>.PointerListToArray(pointerList));
            }
        }

    }
}
