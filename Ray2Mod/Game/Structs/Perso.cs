using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Perso
    {
        public int p3dData;
        public StandardGame* stdGamePtr;
        public int dynam;
        public Brain * brain;
    } 
}