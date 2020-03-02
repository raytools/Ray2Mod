using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StandardGame
    {
        public int familyID;
        public int modelID;
        public int instanceID;
        public SuperObject* superObjectPtr;
    }
}