using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StandardGame
    {
        public int familyID;
        public int modelID;
        public int instanceID;
        public IntPtr superObjectPtr;
    }
}