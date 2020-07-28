using System.Numerics;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Dynamics {

    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixDynamics {
        public double m_1_1;
        public double m_2_1;
        public double m_3_1;
        public double m_4_1;

        public double m_1_2;
        public double m_2_2;
        public double m_3_2;
        public double m_4_2;

        public double m_1_3;
        public double m_2_3;
        public double m_3_3;
        public double m_4_3;

        public double m_1_4;
        public double m_2_4;
        public double m_3_4;
        public double m_4_4;

        public static MatrixDynamics Identity() => new MatrixDynamics
        {
            m_1_1 = 1,
            m_2_2 = 1,
            m_3_3 = 1,
            m_4_4 = 1,
        };

        public Matrix4x4 TransformationMatrix
        {
            get
            {
                Matrix4x4 m = new Matrix4x4(
                    (float)m_1_1, (float)m_1_2, (float)m_1_3, (float)m_1_4,
                    (float)m_2_1, (float)m_2_2, (float)m_2_3, (float)m_2_4,
                    (float)m_3_1, (float)m_3_2, (float)m_3_3, (float)m_3_4,
                    (float)m_4_1, (float)m_4_2, (float)m_4_3, (float)m_4_4);
                return m;
            }

            set
            {
                m_1_1 = value.M11;
                m_1_2 = value.M12;
                m_1_3 = value.M13;
                m_1_4 = value.M13;

                m_2_1 = value.M21;
                m_2_2 = value.M22;
                m_2_3 = value.M23;
                m_2_4 = value.M24;

                m_3_1 = value.M31;
                m_3_2 = value.M32;
                m_3_3 = value.M33;
                m_3_4 = value.M34;

                m_4_1 = value.M41;
                m_4_2 = value.M42;
                m_4_3 = value.M43;
                m_4_4 = value.M44;
            }
        }
    }
}