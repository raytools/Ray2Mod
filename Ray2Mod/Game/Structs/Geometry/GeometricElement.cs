using Ray2Mod.Game.Structs.Material;
using Ray2Mod.Utils;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Geometry
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GeometricElementTriangles
    {
        public GameMaterial* material;
        public ushort numTriangles;
        public ushort numUVs;
        public Triangle* triangles;
        public int uvMappings;
        public Vector3* normals;
        public UV* uvs;
        public int* offVertexIndices;
        public ushort numVertexIndices;
        public ushort parallelBox;
        public int unknown0;

        public UV[] GetUVs()
        {
            var uvsArray = new UV[numUVs];
            for (int i = 0; i < numUVs; i++)
            {
                uvsArray[i] = uvs[i];
            }

            return uvsArray;
        }

        public Triangle[] GetTriangles()
        {
            var trianglesArray = new Triangle[numTriangles];
            for (int i = 0; i < numTriangles; i++)
            {
                trianglesArray[i] = triangles[i];
            }

            return trianglesArray;
        }

        public Vector3[] GetNormals()
        {
            var normalsArray = new Vector3[numTriangles * 3];
            for (int i = 0; i < numTriangles * 3; i++)
            {
                normalsArray[i] = normals[i];
            }

            return normalsArray;
        }

        public void SetNormals(Vector3[] value)
        {
            normals = Memory.ToUnmanagedArray(value);
        }

        public void SetTriangles(Triangle[] value)
        {
            numTriangles = checked((ushort)value.Length);
            triangles = Memory.ToUnmanagedArray(value);
        }

        public void SetUVS(UV[] value)
        {
            numUVs = checked((ushort)value.Length);
            uvs = Memory.ToUnmanagedArray(value);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UV
    {
        public float u, v;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Triangle
    {
        public ushort v0;
        public ushort v1;
        public ushort v2;
    }
}
