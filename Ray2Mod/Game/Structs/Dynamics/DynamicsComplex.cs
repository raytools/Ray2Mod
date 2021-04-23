using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Dynamics.Blocks;
using Ray2Mod.Game.Structs.MathStructs;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DynamicsComplex
    {
        public DynamicsBlockBase DynamicsBlockBaseBase;
        public DynamicsBlockAdvanced DynamicsBlockAdvanced;
        public DynamicsBlockComplex DynamicsBlockComplex;
    }
}
