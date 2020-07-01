using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.EngineObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs
{
    public unsafe struct AlwaysObjects
    {
        public int numAlways;
        public ListItem* header; // first item is not really a list item, but a header

        public struct ListItem
        {
            public ListItem* next;
            public ListItem* prev;
            public int header;
            public int index;
            public Perso* data;
        }

        public List<Pointer<Perso>> GetAlwaysObjects()
        {
            var result = new List<Pointer<Perso>>();

            ListItem* currentItem = header->next;

            while (currentItem != null)
            {
                Perso* perso = currentItem->data;
                result.Add(perso);
                currentItem = currentItem->next;
            }

            return result;
        }

        //public void Append()
    }
}