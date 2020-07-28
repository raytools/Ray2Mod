using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Utils;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sector
    {
        public LinkedList.HasHeaderPointers_ElementPointerFirst<LinkedListPointer<SuperObject>> persoSPOList;

        public LinkedList.HasHeaderPointers<int> staticLightsList;
        public LinkedList.HasHeaderPointers<int> dynamicLightsList;

        public LinkedList.NeighborGraphicSectorList linkedListGraphicSectors;
        public LinkedList.NeighborCollisionSectorList linkedListCollisionSector;
        public LinkedList.NoPreviousPointers_ElementPointerFirst<int> linkedListActivitySector;
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
            SuperObject* spo = new SuperObject().ToUnmanaged();
            Sector* spoData = new Sector().ToUnmanaged();

            spo->SectorData = spoData;

            return spo;
        }
    }
}