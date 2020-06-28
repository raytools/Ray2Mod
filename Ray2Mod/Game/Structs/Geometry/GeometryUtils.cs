using JeremyAnsel.Media.WavefrontObj;
using Ray2Mod.Game.Structs.Material;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ray2Mod.Game.Structs.Geometry
{
    public static unsafe class GeometryUtils
    {
        /// <summary>
        /// Imports a Wavefront .obj file into a GeometricObject. Please note only triangulated meshes are supported.
        /// </summary>
        /// <param name="objPath">The file path to the .obj file</param>
        /// <returns></returns>
        public static GeometricObject* ImportObj(string objPath, GeometricObject* original, GameMaterial* material, RemoteInterface ri)
        {
            GeometricObject* go = original;

            GeometricElementTriangles* gt = (GeometricElementTriangles*)go->off_elements[0];// new GeometricElementTriangles().ToUnmanaged();

            gt->material = material;

            var file = ObjFile.FromFile(objPath);

            var verts = file.Vertices.Select(v =>
            {
                return new Vector3(v.Position.X, v.Position.Y, v.Position.Z);
            });

            // We want the normals to have the same indices as the vertices, so generate a new list.
            List<Vector3> normals = new List<Vector3>();
            List<Triangle> triangles = new List<Triangle>();
            List<UV> uvs = new List<UV>();
            List<UvMapping> uvMappings = new List<UvMapping>();

            short i = 0;
            foreach (var f in file.Faces)
            {
                if (f.Vertices.Count > 3)
                {
                    throw new InvalidDataException("Quad or N-gon detected while importing .obj, make sure to triangulate the mesh before importing.");
                }

                if (f.Vertices.Count < 3)
                {
                    throw new InvalidDataException("Face with less than 3 vertices detected while importing .obj, make sure all faces are triangles!");
                }

                var normal_0 = file.VertexNormals[f.Vertices[0].Normal - 1]; // Wavefront uses 1-based indexing :(
                var normal_1 = file.VertexNormals[f.Vertices[1].Normal - 1];
                var normal_2 = file.VertexNormals[f.Vertices[2].Normal - 1];

                normals.Add(new Vector3(normal_0.X, normal_0.Y, normal_0.Z));
                normals.Add(new Vector3(normal_1.X, normal_1.Y, normal_1.Z));
                normals.Add(new Vector3(normal_2.X, normal_2.Y, normal_2.Z));

                var textureVertex_0 = file.TextureVertices[f.Vertices[0].Texture - 1];
                var textureVertex_1 = file.TextureVertices[f.Vertices[1].Texture - 1];
                var textureVertex_2 = file.TextureVertices[f.Vertices[2].Texture - 1];

                uvs.Add(new UV() { u = textureVertex_0.X, v = textureVertex_0.Y });
                uvs.Add(new UV() { u = textureVertex_1.X, v = textureVertex_1.Y });
                uvs.Add(new UV() { u = textureVertex_2.X, v = textureVertex_2.Y });

                uvMappings.Add(new UvMapping()
                {
                    mapping_0 = (short)(i + 0),
                    mapping_1 = (short)(i + 1),
                    mapping_2 = (short)(i + 2),
                });

                triangles.Add(new Triangle()
                {
                    v0 = (ushort)(f.Vertices[0].Vertex - 1),
                    v1 = (ushort)(f.Vertices[1].Vertex - 1),
                    v2 = (ushort)(f.Vertices[2].Vertex - 1)
                });

                i += 3;
            };

            var vertsArray = verts.ToArray();
            var normalArray = normals.ToArray();
            var triArray = triangles.ToArray();
            var uvArray = uvs.ToArray();

            gt->SetTriangles(triArray);
            gt->SetUVS(uvArray);
            gt->SetUVMappings(uvMappings.ToArray());
            gt->SetNormals(normalArray);
            go->SetVertices(vertsArray);
            go->SetNormals(new Vector3[vertsArray.Length]); // empty normals for geometric object

            var types = new GeometricObject.ElementType[]
            {
                GeometricObject.ElementType.Triangles
            };

            go->SetGeometricElementTypes(types);

            var elements = new int[] {
                (int)gt
            };

            go->SetGeometricElements(elements);

            return go;
        }
    }
}