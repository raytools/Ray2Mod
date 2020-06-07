using Ray2Mod.Game.Structs.LinkedLists;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Sector {

        [LinkedList.Type(typeof(Perso))]
        public LinkedList.HasHeaderPointers linkedListPerso;

        public LinkedList.HasHeaderPointers linkedListStaticLights;
        public LinkedList.HasHeaderPointers linkedListDynamicLights;

        public LinkedList.NeighborSectorList linkedListGraphicSectors;
        public LinkedList.NeighborSectorList linkedListCollisionSector;
        public LinkedList.NoPreviousPointersForDouble_ElementPointerFirst_ReadAtPointer linkedListActivitySector;
        public LinkedList.HasHeaderPointers linkedListSoundSectors;
        public LinkedList.HasHeaderPointers placeholder;

    }
}