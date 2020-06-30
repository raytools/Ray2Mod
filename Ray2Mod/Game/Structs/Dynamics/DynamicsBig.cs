﻿using Ray2Mod.Game.Structs.MathStructs;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct DynamicsBig
    {
        [FieldOffset(0x0)]
        public Dynamics dynamicsBase;

        [FieldOffset(0x54)]
        public Vector3* speedVector;

        [FieldOffset(0x78)]
        public MatrixDynamics matrixA;

        [FieldOffset(0xD0)]
        public MatrixDynamics matrixB;
    }
}