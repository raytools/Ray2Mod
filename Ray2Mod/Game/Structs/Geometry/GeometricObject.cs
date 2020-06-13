using Ray2Mod.Game.Structs.Geometry;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GeometricObject {
        public Vector3* off_vertices;
        public Vector3* off_normals;
        public int* off_materials;
        public int unknown0;
        public ElementType* off_element_types;
        public int* off_elements;
        public int unknown1;
        public int unknown2;
        public int unknown3;
        public int unknown4;
        public uint lookAtMode;
        public ushort numVertices;
        public ushort numElements;
        public int unknown5;
        public float boundingVolumeRadius;
        public Vector3 boundingVolume; // x, y, z
        public int unknown6;
        public int unknown7;
        public float float1;
        public float float2;
        public float float3;
        public float float4;

        public enum ElementType : short {
            Triangles = 1,
            Sprite = 3,
            Bones1 = 13,
            Bones2 = 15,
        }

        public Vector3[] Vertices {
            get
            {
                Vector3[] verts = new Vector3[numVertices];
                for (int i=0;i<numVertices;i++) {
                    verts[i] = off_vertices[i];
                }
                return verts;
            }
            set
            {
                if (value.Length > ushort.MaxValue) {
                    throw new OverflowException($"Maximum number of vertices for GeometricObject is {ushort.MaxValue}, array length was {value.Length}");
                }
                numVertices = (ushort)value.Length;
                for (int i = 0; i < value.Length; i++) {
                    off_vertices[i] = value[i];
                }
            }
        }

        public Vector3[] Normals {
            get
            {
                Vector3[] normals = new Vector3[numVertices];
                for (int i = 0; i < numVertices; i++) {
                    normals[i] = off_normals[i];
                }
                return normals;
            }
            set
            {
                if (value.Length > ushort.MaxValue) {
                    throw new OverflowException($"Maximum number of normals (vertices) for GeometricObject is {ushort.MaxValue}, array length was {value.Length}");
                }
                numVertices = (ushort)value.Length;
                for (int i = 0; i < value.Length; i++) {
                    off_normals[i] = value[i];
                }
            }
        }

        public ElementType[] GeometricElementTypes
        {
            get
            {
                ElementType[] types = new ElementType[numElements];
                for (int i=0;i<numElements;i++) {
                    types[i] = off_element_types[i];
                }

                return types;
            }
        }

        public GeometricElementTriangles[] GeometricElementsAsTriangles {
            get
            {
                GeometricElementTriangles[] result = new GeometricElementTriangles[numElements];

                for(int i=0;i<numElements;i++) {
                    GeometricElementTriangles data = (GeometricElementTriangles)Marshal.PtrToStructure((IntPtr)off_elements[i], typeof(GeometricElementTriangles));
                    result[i] = data;
                }

                return result;
            }
        }

        public void RecalculateNormals()
        {
            var types = GeometricElementTypes;
            var vertices = Vertices;

            for (int i = 0; i < numElements; i++) {

                if (types[i]!=ElementType.Triangles) {
                    continue;
                }

                var tris = GeometricElementsAsTriangles[i];
                var normals = tris.Normals;

                for (int j = 0; j < tris.numTriangles; j++) {

                    Triangle triangle = tris.triangles[j];

                    Vector3 pointA = vertices[triangle.v0];
                    Vector3 pointB = vertices[triangle.v1];
                    Vector3 pointC = vertices[triangle.v2];

                    Vector3 oldNormal = tris.normals[j];
                    Vector3 autoNormal = (pointB - pointA).Cross(pointC - pointA).Normalized();

                    normals[j] = autoNormal;
                }

                tris.Normals = normals;
            }
        }
    }

}