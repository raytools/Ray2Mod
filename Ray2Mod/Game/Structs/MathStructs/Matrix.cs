using System.Numerics;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.MathStructs
{
    // Sizeof = 0x58
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix
    {
        public int transformationType;
        public Vector3 position;

        // Row_Column

        public float m_1_1;
        public float m_1_2;
        public float m_1_3;
        public float m_2_1;
        public float m_2_2;
        public float m_2_3;
        public float m_3_1;
        public float m_3_2;
        public float m_3_3;
        public float scale_1_1;
        public float scale_1_2;
        public float scale_1_3;
        public float scale_2_1;
        public float scale_2_2;
        public float scale_2_3;
        public float scale_3_1;
        public float scale_3_2;
        public float scale_3_3;


        public static Matrix IdentityMatrix = new Matrix()
        {
            transformationType = 1,
            m_1_1 = 1,
            m_2_2 = 1,
            m_3_3 = 1,
            scale_1_1 = 1,
            scale_2_2 = 1,
            scale_3_3 = 1
        };

        public Matrix4x4 TransformationMatrix
        {
            get
            {
                Matrix4x4 m = new Matrix4x4(m_1_1, m_1_2, m_1_3, 0, m_2_1, m_2_2, m_2_3, 0, m_3_1, m_3_2, m_3_3, 0, position.x, position.y, position.z, 0);
                return m;
            }

            set
            {
                m_1_1 = value.M11;
                m_1_2 = value.M12;
                m_1_3 = value.M13;

                m_2_1 = value.M21;
                m_2_2 = value.M22;
                m_2_3 = value.M23;

                m_3_1 = value.M31;
                m_3_2 = value.M32;
                m_3_3 = value.M33;

                position.x = value.M41;
                position.y = value.M42;
                position.z = value.M43;
            }
        }

        public Matrix4x4 ScaleMatrix
        {
            get
            {
                Matrix4x4 m = new Matrix4x4(scale_1_1, scale_1_2, scale_1_3, 0, scale_2_1, scale_2_2, scale_2_3, 0, scale_3_1, scale_3_2, scale_3_3, 0, 0, 0, 0, 1);
                return m;
            }

            set
            {
                scale_1_1 = value.M11;
                scale_1_2 = value.M12;
                scale_1_3 = value.M13;

                scale_2_1 = value.M21;
                scale_2_2 = value.M22;
                scale_2_3 = value.M23;

                scale_3_1 = value.M31;
                scale_3_2 = value.M32;
                scale_3_3 = value.M33;
            }
        }
    }
}