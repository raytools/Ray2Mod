﻿using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Utils;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sector
    {

        public LinkedList.HasHeaderPointers_ElementPointerFirst<SuperObject> persoSPOList;

        public LinkedList.HasHeaderPointers<int> staticLightsList;
        public LinkedList.HasHeaderPointers<int> dynamicLightsList;

        public LinkedList.NeighborSectorList linkedListGraphicSectors;
        public LinkedList.NeighborSectorList linkedListCollisionSector;
        public LinkedList.NoPreviousPointersForDouble_ElementPointerFirst_ReadAtPointer<int> linkedListActivitySector;
        public LinkedList.HasHeaderPointers<int> linkedListSoundSectors;
        public LinkedList.HasHeaderPointers<int> placeholder;
        public BoundingVolumeBox boundingBox;
        public int unknown0;
        public byte isVirtual;
        public byte unknown1;
        public byte unknown2;
        public byte sectorPriority;
        public int* skyMaterial;
        public byte unknown3;

        public static SuperObject* CreateSectorSuperObject()
        {
            var spo = new SuperObject().ToUnmanaged();
            var spoData = new Sector().ToUnmanaged();

            spo->SectorData = spoData;

            return spo;
        }
    }
}