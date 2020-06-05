using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Behavior
    {
        public int scriptsPointer;
        public int firstScript;
        public byte numScripts;
        public byte field_A;
        public byte field_B;
        public byte field_C;
    }

    public unsafe struct BehaviorArray
    {
        public Behavior* Array;
        public int Length;
    }
}