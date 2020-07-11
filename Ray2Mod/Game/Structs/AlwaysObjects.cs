using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.EngineObject;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs
{
    public unsafe struct AlwaysObjects
    {
        public struct AlwaysPersoListItem
        {
            public int modelID;
            public Perso* perso;
        }

        public int numAlways; // Maximum number of always objects that can be spawned
        public LinkedList.HasHeaderPointers<AlwaysPersoListItem> alwaysPersos;
        public SuperObject* alwaysSuperObjects;
        public int* alwaysStructuresMaybe;
        public Perso* reusablePersos;

        public struct ListItem
        {
            public ListItem* next;
            public ListItem* prev;
            public int header;
            public int index;
            public Perso* data;
        }

        public Perso*[] ReusablePersos
        {
            get
            {
                Perso*[] result = new Perso*[numAlways];
                for (int i = 0; i < numAlways; i++)
                {
                    result[i] = &reusablePersos[i];
                }

                return result;
            }
        }

        public void AddAlwaysPerso(Perso* perso) {
            alwaysPersos.Add(new AlwaysPersoListItem() {
                modelID = perso->stdGamePtr->modelID,
                perso = perso
            });
        }
    }
}