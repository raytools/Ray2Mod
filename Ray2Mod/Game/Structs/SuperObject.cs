using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.LinkedLists;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SuperObject
    {
        public SuperObjectType type;
        public int* engineObjectPtr;

        //linked list
        public LinkedList.SuperObjectList children;

        public SuperObject* nextBrother;
        public SuperObject* previousBrother;
        public SuperObject* parent;
        public Matrix* matrixPtr;
        public Matrix* matrixPtr2;

        private int padding1;
        private int padding2;

        public int flags;
    }

    public enum SuperObjectType {
        Unknown = 0x0,
        World = 0x1,
        Perso = 0x2,
        Sector = 0x4,
        PhysicalObject = 0x8,
        IPO = 0x20,
        IPO_2 = 0x40,
        GeometricObject = 0x400, // Geometric Object
        GeometricShadowObject = 0x80000, // Instantiated Geometric Object
    }
}