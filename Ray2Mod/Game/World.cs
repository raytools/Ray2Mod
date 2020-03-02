using System.Collections.Generic;
using System.Text;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;
using Ray2Mod.Utils;

namespace Ray2Mod.Game
{
    public unsafe class World
    {
        public Dictionary<ObjectSet, string[]> ObjectNames { get; private set; }

        public Dictionary<string, Pointer<SuperObject>> GetActiveSuperObjects()
        {
            const int offDynamWorld = 0x0500FD0;
            Dictionary<string, Pointer<SuperObject>> result = new Dictionary<string, Pointer<SuperObject>>();

            SuperObject* superObject = (SuperObject*)Memory.GetPointerAtOffset(offDynamWorld, 0x8, 0x0);

            while (superObject != null)
            {
                Perso* perso = (Perso*)superObject->engineObjectPtr;
                if (perso != null)
                {
                    StandardGame* offStdGame = perso->stdGamePtr;
                    int nameIndex = offStdGame->instanceID;
                    string name = $"unknown_{(int)superObject:X}";

                    if (nameIndex >= 0 && nameIndex < ObjectNames[ObjectSet.Instance].Length)
                        name = ObjectNames[ObjectSet.Instance][nameIndex];

                    if (!result.ContainsKey(name))
                        result.Add(name, superObject);
                }

                superObject = superObject->nextBrother;
            }

            return result;
        }

        public void ReadObjectNames()
        {
            const int offObjectTypes = 0x005013E0;
            ObjectNames = new Dictionary<ObjectSet, string[]>();

            for (int i = 0; i < 3; i++)
            {
                int* offNamesHeader = (int*)(offObjectTypes + (i * 12));
                int offNamesFirst = *offNamesHeader;
                int offNamesLast = *(offNamesHeader + 1);
                int numNames = *(offNamesHeader + 2);

                ObjectNames[(ObjectSet)i] = ReadObjectNamesTable((int*)offNamesFirst, numNames);
            }
        }

        private string[] ReadObjectNamesTable(Pointer<int> offNamesFirst, int numNames)
        {
            int* currentOffset = offNamesFirst;
            string[] names = new string[numNames];

            for (int j = 0; j < numNames; j++)
            {

                int* offNamesNext = (int*)*currentOffset;
                byte* offName = (byte*)*(currentOffset + 3);

                byte[] nameBytes = new byte[64];
                for (int i = 0; i < nameBytes.Length && offName[i] != 0; i++)
                    nameBytes[i] = offName[i];
                names[j] = Encoding.GetEncoding(1252).GetString(nameBytes).Trim('\0');

                if (offNamesNext != null)
                {
                    currentOffset = offNamesNext;
                }
            }

            return names;
        }

    }

    public enum ObjectSet
    {
        Family = 0,
        Model = 1,
        Instance = 2
    }
}