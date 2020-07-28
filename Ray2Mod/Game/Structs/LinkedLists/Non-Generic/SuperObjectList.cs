﻿using System.Runtime.InteropServices;

using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.SPO;

namespace Ray2Mod.Game.Structs.LinkedLists
{
    public abstract unsafe partial class LinkedList
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SuperObjectList
        {
            public SuperObject* Head;
            public SuperObject* Tail;
            public int Count;

            public SuperObject*[] Read()
            {
                SuperObject*[] results = new SuperObject*[Count];

                // List not empty?
                if (Head != null)
                {

                    SuperObject* Next = Head;

                    for (int i = 0; i < Count; i++)
                    {
                        SuperObject* Element = Next;
                        Next = Next->nextBrother;
                        // Previous pointer is ignored

                        results[i] = Element;
                    }

                }

                return results;
            }

            public void Add(SuperObject* newSuperObject)
            {
                if (Count == 0)
                {
                    Head = Tail = newSuperObject;
                }
                else
                {
                    newSuperObject->previousBrother = Tail;
                    newSuperObject->parent = Tail->parent;
                    Tail->nextBrother = newSuperObject;
                    Tail = newSuperObject;
                }

                Count++;
            }

            public void Remove(SuperObject* superObjectToRemove)
            {
                SuperObject*[] oldList = Read();
                SuperObject*[] newList = oldList.Where(i => i != superObjectToRemove);
                Write(newList);
            }

            public void Write(SuperObject*[] superObjects)
            {
                // First read the old list and clear their next and previous brother
                SuperObject*[] oldList = Read();
                foreach (SuperObject* oldSPO in oldList)
                {
                    oldSPO->previousBrother = null;
                    oldSPO->nextBrother = null;
                }

                Count = superObjects.Length;
                Head = superObjects[0];
                Tail = superObjects[Count - 1];

                for (int i = 0; i < Count; i++)
                {
                    if (i < Count - 1)
                    {
                        superObjects[i]->nextBrother = superObjects[i + 1];
                    }
                    if (i > 0)
                    {
                        superObjects[i]->previousBrother = superObjects[i - 1];
                    }
                }
            }
        }
    }
}