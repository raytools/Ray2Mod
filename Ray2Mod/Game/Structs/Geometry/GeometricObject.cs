using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Utils;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GeometricObject
    {
        public Vector3* off_vertices; // 0x0
        public Vector3* off_normals; // 0x4
        public int* off_materials; // 0x8
        public int unknown0; // 0xC
        public short* off_element_types; // 0x10
        public int* off_elements; // 0x14
        public int* unknown1; // 0x18
        public int unknown2; // 0x1C
        public int unknown3; // 0x20
        public int unknown4; // 0x24
        public uint lookAtMode; // 0x28
        public ushort numVertices; // 0x2C
        public ushort numElements; // 0x2E
        public int unknown5; // 0x30
        public float boundingSphereRadius; // 0x34
        public Vector3 boundingSphereCenter; // x, y, z

        public enum ElementType : short
        {
            Triangles = 1,
            Sprite = 3,
            Bones1 = 13,
            Bones2 = 15,
        }

        /// <summary>
        /// Generates a new array of vertices.
        /// </summary>
        /// <returns>The array of vertices.</returns>
        public Vector3[] GetVertices()
        {
            Vector3[] verts = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                verts[i] = off_vertices[i];
            }
            return verts;
        }

        /// <summary>
        /// Replaces the vertices with the new specified array and updates the numVertices field.
        /// </summary>
        /// <param name="value">The array of vertices (maximum length of 2^16)</param>
        public void SetVertices(Vector3[] value)
        {
            numVertices = checked((ushort)value.Length);
            off_vertices = Memory.ToUnmanagedArray(value);
        }

        /// <summary>
        /// Generates a new array of normals.
        /// </summary>
        /// <returns>The array of normals.</returns>
        public Vector3[] GetNormals()
        {
            Vector3[] normals = new Vector3[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                normals[i] = off_normals[i];
            }
            return normals;
        }

        /// <summary>
        /// Replaces the normals with the new specified array and updates the numVertices field.
        /// </summary>
        /// <param name="value">The array of normals (maximum length of 2^16)</param>
        public void SetNormals(Vector3[] value)
        {
            numVertices = checked((ushort)value.Length);
            off_normals = Memory.ToUnmanagedArray(value);
        }

        public ElementType[] GetGeometricElementTypes()
        {
            ElementType[] types = new ElementType[numElements];
            for (int i = 0; i < numElements; i++)
            {
                types[i] = (ElementType)off_element_types[i];
            }

            return types;
        }

        /// <summary>
        /// Return an array containing pointers to Geometric Elements
        /// </summary>
        /// <returns>An array of pointers (ints) to Geometric Elements. Typecast them to their respective types.</returns>
        public int[] GetGeometricElements()
        {
            int[] result = new int[numElements];

            for (int i = 0; i < numElements; i++)
            {
                result[i] = off_elements[i];
            }

            return result;
        }

        public void SetGeometricElementTypes(ElementType[] value)
        {
            numElements = checked((ushort)value.Length);
            short[] convertedArray = new short[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                convertedArray[i] = (short)value[i];
            }
            off_element_types = Memory.ToUnmanagedArray(convertedArray);
        }

        public void SetGeometricElements(int[] value)
        {
            numElements = checked((ushort)value.Length);
            off_elements = Memory.ToUnmanagedArray(value);
        }

        /// <summary>
        /// Returns an AABB that contains all vertices.
        /// </summary>
        /// <returns>A bounding box that contains all vertices.</returns>
        public BoundingVolumeBox GetVertexBounds()
        {
            var vertices = GetVertices();
            float minX = vertices.Select(v => v.x).Min();
            float minY = vertices.Select(v => v.y).Min();
            float minZ = vertices.Select(v => v.z).Min();

            float maxX = vertices.Select(v => v.x).Max();
            float maxY = vertices.Select(v => v.y).Max();
            float maxZ = vertices.Select(v => v.z).Max();

            return new BoundingVolumeBox()
            {
                boxMin = new Vector3(minX, minY, minZ),
                boxMax = new Vector3(maxX, maxY, maxZ),
            };
        }

        /// <summary>
        /// Recalculate all normals
        /// </summary>
        public void RecalculateNormals(RemoteInterface ri)
        {
            var types = GetGeometricElementTypes();
            var vertices = GetVertices();

            for (int i = 0; i < numElements; i++)
            {
                if (types[i] != ElementType.Triangles)
                {
                    continue;
                }

                var tris = (GeometricElementTriangles*)off_elements[i];

                for (int j = 0; j < tris->numTriangles; j++)
                {
                    Triangle triangle = tris->triangles[j];

                    Vector3 pointA = vertices[triangle.v0];
                    Vector3 pointB = vertices[triangle.v1];
                    Vector3 pointC = vertices[triangle.v2];

                    Vector3 oldNormal = tris->normals[j];
                    Vector3 autoNormal = (pointB - pointA).Cross(pointC - pointA).Normalized();

                    ri.Log($"Old normal: {oldNormal}");
                    ri.Log($"Auto normal: {autoNormal}");

                    tris->normals[j] = autoNormal;
                }
            }
        }

        public void RecalculateBoundingSphere()
        {
            var sphere = GetVertexBounds().CreateBoundingSphere();
            boundingSphereCenter = sphere.center;
            boundingSphereRadius = sphere.radius;
        }
    }
}