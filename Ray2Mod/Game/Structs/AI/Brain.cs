﻿using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Brain
    {
        public Mind* mind;
    }
}