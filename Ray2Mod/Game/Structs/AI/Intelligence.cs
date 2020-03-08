using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI {

    // Intelligence is basically a state engine with one active state (behavior/comport) at a time.

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Intelligence {
        public AIModel* aiModel;
        public IntPtr actionTree; // ?
        public Behavior* currentBehavior;
    }
}