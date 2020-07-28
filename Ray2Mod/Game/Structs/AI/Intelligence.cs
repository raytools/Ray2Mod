using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Intelligence
    {
        public AIModel* aiModel;
        public int* actionTree;
        public Behavior* behavior;
        public Behavior* lastBehavior;
        public int* actionTable;
    }
}