using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

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

            public unsafe SuperObject*[] Read()
            {
                SuperObject*[] results = new SuperObject*[Count];

                SuperObject* Next = Head;

                for (int i = 0; i < Count; i++)
                {
                    SuperObject* Element = Next;
                    Next = Next->nextBrother;
                    // Previous pointer is ignored

                    results[i] = (SuperObject*)(Element);
                }

                return results;
            }

            public unsafe void Append(SuperObject* newSuperObject)
            {
                newSuperObject->previousBrother = Tail;
                Tail->nextBrother = newSuperObject;
                Tail = newSuperObject;
            }

            public unsafe void Remove(SuperObject* superObjectToRemove)
            {
                var oldList = Read();
                var newList = oldList.Where(i => i != superObjectToRemove);
                Write(newList);
            }

            public unsafe void Write(SuperObject*[] superObjects)
            {
                // First read the old list and clear their next and previous brother
                var oldList = Read();
                foreach (var oldSPO in oldList)
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