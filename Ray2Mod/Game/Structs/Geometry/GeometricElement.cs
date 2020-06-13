using Ray2Mod.Game.Structs;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Geometry {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GeometricElementTriangles {
        public int* material;
        public ushort numTriangles;
        public ushort numUVs;
        public Triangle* triangles;
        public UVMapping* uvMappings;
        public Vector3* normals;
        public UV* uvs;
        public int* offVertexIndices;
        public ushort numVertexIndices;
        public ushort parallelBox;
        public int unknown0;


        public UVMapping[] MappingUVS {
            get
            {
                var uvMappingsArray = new UVMapping[numTriangles];
                for (int i = 0; i < numTriangles; i++) {
                    uvMappingsArray[i] = uvMappings[i];
                }

                return uvMappingsArray;
            }
        }

        public UV[] UVs {
            get
            {
                var uvsArray = new UV[numTriangles];
                for (int i = 0; i < numTriangles; i++) {
                    uvsArray[i] = uvs[i];
                }

                return uvsArray; 
            }
        }

        public Triangle[] Triangles{
            get
            {
                var trianglesArray = new Triangle[numTriangles];
                for (int i = 0; i < numTriangles; i++) {
                    trianglesArray[i] = triangles[i];
                }

                return trianglesArray;
            }
        }

        public Vector3[] Normals{
            get
            {
                var normalsArray = new Vector3[numTriangles];
                for (int i = 0; i < numTriangles; i++) {
                    normalsArray[i] = normals[i];
                }

                return normalsArray;
            }

            set
            {
                if (value.Length > ushort.MaxValue) {
                    throw new OverflowException($"Maximum number of normals (vertices) for GeometricObject is {ushort.MaxValue}, array length was {value.Length}");
                }
                numTriangles = (ushort)value.Length;
                for (int i = 0; i < value.Length; i++) {
                    normals[i] = value[i];
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UV {
        public float u, v;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Triangle {
        public ushort v0;
        public ushort v1;
        public ushort v2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UVMapping {
        public ushort v0;
        public ushort v1;
        public ushort v2;
    }
}
