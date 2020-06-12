using Ray2Mod.Game.Structs.LinkedLists;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sector {

        public LinkedList.HasHeaderPointers_ElementPointerFirst<SuperObject> persoSPOList;

        public LinkedList.HasHeaderPointers<int> staticLightsList;
        public LinkedList.HasHeaderPointers<int> dynamicLightsList;

        public LinkedList.NeighborSectorList linkedListGraphicSectors;
        public LinkedList.NeighborSectorList linkedListCollisionSector;
        public LinkedList.NoPreviousPointersForDouble_ElementPointerFirst_ReadAtPointer<int> linkedListActivitySector;
        public LinkedList.HasHeaderPointers<int> linkedListSoundSectors;
        public LinkedList.HasHeaderPointers<int> placeholder;

    }
}