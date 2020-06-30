using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Dynamics
    {
        public long field_0x0_counter;
        public int field_0x8_stateFlags1;
        public int field_0xC_stateFlags2;
        public float field_0x10_gravity;

        public DynamicsType Type
        {
            get
            {
                if ((field_0xC_stateFlags2 & 4) != 0)
                {
                    return DynamicsType.Big;
                }
                else if ((field_0xC_stateFlags2 & 2) != 0)
                {
                    return DynamicsType.Medium;
                }
                else
                {
                    return DynamicsType.Small;
                }
            }
        }

        public enum DynamicsType
        {
            Small = 0,
            Medium = 1,
            Big = 2,
        }
    }
}