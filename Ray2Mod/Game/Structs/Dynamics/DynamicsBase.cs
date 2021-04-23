using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Dynamics.Blocks;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DynamicsBase
    {
        public DynamicsBlockBase DynamicsBlockBase;
    }
}
