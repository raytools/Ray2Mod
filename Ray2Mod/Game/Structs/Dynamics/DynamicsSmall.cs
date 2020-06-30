using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct DynamicsSmall
    {
        [FieldOffset(0x0)]
        public Dynamics dynamicsBase;
    }
}