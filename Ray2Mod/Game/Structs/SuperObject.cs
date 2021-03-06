﻿using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SuperObject
    {
        public int type;
        public IntPtr engineObjectPtr;

        //linked list
        public fixed byte children[12];

        public SuperObject* nextBrother;
        public SuperObject* previousBrother;
        public SuperObject* parent;
        public Matrix* matrixPtr;
        public Matrix* matrixPtr2;

        private int padding1;
        private int padding2;

        public int flags;
    }
}