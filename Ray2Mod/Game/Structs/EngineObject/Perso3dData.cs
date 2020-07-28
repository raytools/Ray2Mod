using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Families;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.States;

namespace Ray2Mod.Game.Structs.EngineObject
{
    // Sizeof = 0x118
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Perso3dData
    {
        [FieldOffset(0x0)]
        public LinkedList.ListElement_HHP<State>* stateInitial;
        [FieldOffset(0x4)]
        public LinkedList.ListElement_HHP<State>* stateCurrent;
        [FieldOffset(0x8)]
        public int* state2;

        [FieldOffset(0xC)]
        public int* objectList;
        [FieldOffset(0x10)]
        public int* objectListInitial;
        [FieldOffset(0x14)]
        public LinkedList.ListElement_HHP<Family>* family;

        // Exactly 0x100 bytes after this
        [FieldOffset(0x18)]
        public fixed char fixedBuffer[0x100];

        [FieldOffset(0x8C)]
        public short currentFrame;

        [FieldOffset(0x8E)]
        public byte actionEnd;

        [FieldOffset(0x8F)]
        public byte animationEnd;
    }
}