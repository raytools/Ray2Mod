using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixDynamics
    {
        public double d_0_0;
        public double d_1_0;
        public double d_2_0;
        public double d_3_0;

        public double d_0_1;
        public double d_1_1;
        public double d_2_1;
        public double d_3_1;

        public double d_0_2;
        public double d_1_2;
        public double d_2_2;
        public double d_3_2;

        public double d_0_3;
        public double d_1_3;
        public double d_2_3;
        public double d_3_3;
    }
}