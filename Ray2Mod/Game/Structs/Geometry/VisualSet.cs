using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Geometry
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VisualSet
    {
        public int field0;
        public short numberOfLOD;
        public short visualSetType;
        public float* off_LODDistances;
        public GeometricObject** off_LODDataOffsets;
        public int alwaysZero1;
        public int alwaysZero2;

        public VisualSetLOD[] VisualSetLODS
        {
            get
            {
                VisualSetLOD[] visualSetLODs = new VisualSetLOD[numberOfLOD];
                for (uint i = 0; i < numberOfLOD; i++)
                {
                    visualSetLODs[i] = new VisualSetLOD
                    {
                        LODdistance = off_LODDistances[i],
                        off_data = off_LODDataOffsets[i]
                    };
                }

                return visualSetLODs;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VisualSetLOD
    {
        public float LODdistance;
        public GeometricObject* off_data;
    }
}