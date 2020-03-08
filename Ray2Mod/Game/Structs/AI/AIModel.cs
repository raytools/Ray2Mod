using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI {


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AIModel {
        public IntPtr normalBehaviorList;
        public IntPtr reflexBehaviorList;
        public DsgVar* dsgVar;
    }
}