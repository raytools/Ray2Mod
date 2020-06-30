using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics
{
    // Sizeof = 0xC
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Dynam
    {
        public Dynamics* dynamics;
        public int* parsingDatas;
        public int* unknown;

        public DynamicsSmall* DynamicsAsSmallDynamics
        {
            get { return ((dynamics != null) ? (DynamicsSmall*)AssertType(Dynamics.DynamicsType.Small) : null); }
        }

        public DynamicsMedium* DynamicsAsMediumDynamics
        {
            get { return ((dynamics != null) ? (DynamicsMedium*)AssertType(Dynamics.DynamicsType.Medium) : null); }
        }

        public DynamicsBig* DynamicsAsBigDynamics
        {
            get { return ((dynamics != null) ? (DynamicsBig*)AssertType(Dynamics.DynamicsType.Big) : null); }
        }

        private Dynamics* AssertType(Dynamics.DynamicsType dynamicsType)
        {
            if (dynamics != null && dynamics->Type != dynamicsType)
            {
                throw new InvalidCastException($"Trying to cast dynamics of type {dynamics->Type} to {dynamicsType}");
            }
            return dynamics;
        }
    }
}