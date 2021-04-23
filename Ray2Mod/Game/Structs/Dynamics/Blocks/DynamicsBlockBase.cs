using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.MathStructs;

namespace Ray2Mod.Game.Structs.Dynamics.Blocks
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DynamicsBlockBase
    {
        public int m_lObjectType;
        public int m_pCurrentIdCard;
        public int ulFlags;
        public int ulEndFlags;
        public float m_xGravity;
        public float m_xSlopeLimit;
        public float m_xCosSlope;
        public float m_xSlide;
        public float m_xRebound;
        public Vector3 m_stImposeSpeed;
        public Vector3 m_stProposeSpeed;
        public Vector3 m_stPreviousSpeed;
        public Vector3 m_stScale;
        public Vector3 m_stSpeedAnim;
        public Vector3 m_stSafeTranslation;
        public Vector3 m_stAddTranslation;
        public Matrix m_stPreviousMatrix;
        public Matrix m_stCurrentMatrix;

        // TODO: make a struct for MTH3D_tdstMatrix
        public Vector3 m_stImposeRotationMatrix1;
        public Vector3 m_stImposeRotationMatrix2;
        public Vector3 m_stImposeRotationMatrix3;

        public byte m_ucNbFrame;
        public void* m_pstReport;

        public DynamicsType Type
        {
            get
            {
                if ((ulEndFlags & 4) != 0)
                {
                    return DynamicsType.Complex;
                }
                else if ((ulEndFlags & 2) != 0)
                {
                    return DynamicsType.Advanced;
                }
                else
                {
                    return DynamicsType.Base;
                }
            }
        }

        public enum DynamicsType
        {
            Base = 0,
            Advanced = 1,
            Complex = 2,
        }
    }
}
