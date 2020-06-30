﻿using Ray2Mod.Components;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Structs.EngineObject;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Structs.SPO;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Ray2Mod.Game
{
    public unsafe class World
    {
        private RemoteInterface remoteInterface;

        public Dictionary<ObjectSet, string[]> ObjectNames { get; private set; } = null;

        public SuperObject* WorldSector
        {
            get
            {
                return *((SuperObject**)(Offsets.FatherSector));
            }
        }

        public SuperObject* ActiveDynamicWorld
        {
            get
            {
                return *((SuperObject**)(Offsets.ActiveDynamicWorld));
            }
        }

        public SuperObject* InactiveDynamicWorld
        {
            get
            {
                return *((SuperObject**)(Offsets.InactiveDynamicWorld));
            }
        }

        public World(RemoteInterface remoteInterface)
        {
            this.remoteInterface = remoteInterface;

            // Read Object Names every frame
            GlobalActions.Engine += ReadObjectNames;
        }

        private World()
        {
        }

        struct ListItem
        {
            public ListItem* next;
            public ListItem* prev;
            public int header;
            public int index;
            public Perso* data;
        }

        public Dictionary<string, Pointer<Perso>> GetAlwaysObjects()
        {
            int* off_NumAlways = (int*)Offsets.NumAlways;
            Dictionary<string, Pointer<Perso>> result = new Dictionary<string, Pointer<Perso>>();

            int numAlways = *off_NumAlways;
            ListItem* currentItem = ((ListItem*)(off_NumAlways + 1))->next; // skip the first item, since it's a header

            while (currentItem != null)
            {
                Perso* perso = currentItem->data;
                result.Add(ObjectNames[ObjectSet.Instance][perso->stdGamePtr->instanceID], perso);
                currentItem = currentItem->next;
            }

            return result;
        }

        /// <summary>
        /// Returns a dictionary of Perso-type SuperObjects with their instance name as their key.
        /// </summary>
        /// <param name="world">The world, either ActiveDynamicWorld or InactiveDynamicWorld</param>
        /// <returns></returns>
        public Dictionary<string, Pointer<SuperObject>> GetSuperObjectsWithNames(SuperObject* world)
        {
            Dictionary<string, Pointer<SuperObject>> result = new Dictionary<string, Pointer<SuperObject>>();

            SuperObject*[] superObjects = world->children.Read();

            foreach (SuperObject* superObject in superObjects)
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
            }

            return result;
        }

        private void ReadObjectNames()
        {
            byte engineState = *(byte*)Offsets.EngineState;

            if (engineState != 9)
            {
                return;
            }

            const int offObjectTypes = Offsets.ObjectTypes;
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

        public int GenerateAlwaysObject(SuperObject* spawnedBy, Perso* alwaysPerso, Vector3 position)
        {
            if (spawnedBy == null)
            {
                throw new NullReferenceException("GenerateAlwaysObject: spawnedBy is not allowed to be null!");
            }

            int[] interp = {
                0x00000042, // Func_GenerateObj
                0x03020000,
                (int)alwaysPerso, // arg0, Perso to generate
                0x17030000, // arg1, Vector3
                0x00000000,
                0x10030000,
                BitConverter.ToInt32(BitConverter.GetBytes(position.x),0), // x
                0x0D040000,
                BitConverter.ToInt32(BitConverter.GetBytes(position.y),0), // y
                0x0D040000,
                BitConverter.ToInt32(BitConverter.GetBytes(position.z),0), // z
                0x0D040000,
            };

            // TODO: use ArrayPtr()

            IntPtr interpArray = Marshal.AllocHGlobal(interp.Length * 4);
            for (int i = 0; i < interp.Length; i++)
            {
                Marshal.WriteInt32(interpArray, i * 4, interp[i]);
            }

            IntPtr paramArray = Marshal.AllocHGlobal(0x20 * 4);

            IntPtr interpPtrStart = interpArray + 0x8; // we start at the second node of the interpreter tree

            EngineFunctions.MiscFunction.Call((int)spawnedBy, (int)interpPtrStart, (int)paramArray);

            return *(int*)paramArray.ToPointer();
        }

        public SuperObject*[] GetSectors()
        {
            return WorldSector->children.Read();
        }
    }

    public enum ObjectSet
    {
        Family = 0,
        Model = 1,
        Instance = 2
    }
}