using System;
using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Dynamics.Blocks;

namespace Ray2Mod.Game.Structs.Dynamics
{
    // Sizeof = 0xC
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Dynam
    {
        public void* p_stDynamics;
        public int* p_stParsingDatas;
        public int eUsedMechanics;

        public DynamicsBase* DynamicsBase => ((p_stDynamics != null) ? (DynamicsBase*)AssertType(DynamicsBlockBase.DynamicsType.Base) : null);

        public DynamicsAdvanced* DynamicsAdvanced => ((p_stDynamics != null) ? (DynamicsAdvanced*)AssertType(DynamicsBlockBase.DynamicsType.Advanced) : null);

        public DynamicsComplex* DynamicsComplex => ((p_stDynamics != null) ? (DynamicsComplex*)AssertType(DynamicsBlockBase.DynamicsType.Complex) : null);

        private void* AssertType(DynamicsBlockBase.DynamicsType dynamicsType)
        {
            if (p_stDynamics == null)
            {
                throw new NullReferenceException($"p_stDynamics is null");
            }

            DynamicsBase* baseDynam = ((DynamicsBase*)p_stDynamics);
            if (baseDynam->DynamicsBlockBase.Type < dynamicsType)
            {
                throw new InvalidCastException($"Trying to upcast dynamics of type {baseDynam->DynamicsBlockBase.Type} to {dynamicsType}");
            }
            return p_stDynamics;
        }
    }
}
