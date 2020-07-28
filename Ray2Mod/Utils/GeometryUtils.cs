using System.Collections.Generic;
using System.IO;
using System.Linq;

using JeremyAnsel.Media.WavefrontObj;

using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Game.Structs.Material;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Types;

namespace Ray2Mod.Utils
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

            GameMaterial mtl = new GameMaterial();

            List<Texture> textures = TextureLoader.GetTextures();
            foreach (Texture t in textures)
            {
                ri.Log($"Texture ptr 0x{(int)t.TexData:X} {t.Name}");
            }

            ObjFile file = ObjFile.FromFile(objPath);

            GeometricElementTriangles*[] elements = new GeometricElementTriangles*[file.Groups.Count];

            for (int i = 0; i < file.Groups.Count; i++)
            {
                ObjGroup objGroup = file.Groups[i];
                GeometricElementTriangles geoElem = new GeometricElementTriangles();

                ri.Log($"OBJECT GROUP {objGroup.Name}");

                GameMaterial newMtl = mtl;
                VisualMaterial vis = new VisualMaterial
                {

                    // TODO: figure out visual material flags
                    flags = 1306265599,
                    ambientCoef = new Vector4(0.3f, 0.3f, 0.3f, 0.3f)
                };

                // Group start

                // We want the normals to have the same indices as the vertices, so generate a new list.
                List<Vector3> normals = new List<Vector3>();
                List<Triangle> triangles = new List<Triangle>();
                List<UV> uvs = new List<UV>();
                List<UvMapping> uvMappings = new List<UvMapping>();

                short vertexCount = 0;
                foreach (ObjFace f in objGroup.Faces)
                {
                    if (f.Vertices.Count > 3)
                    {
                        throw new InvalidDataException("Quad or N-gon detected while importing .obj, make sure to triangulate the mesh before importing.");
                    }

                    if (f.Vertices.Count < 3)
                    {
                        throw new InvalidDataException("Face with less than 3 vertices detected while importing .obj, make sure all faces are triangles!");
                    }

                    ObjVector3 normal_0 = file.VertexNormals[f.Vertices[0].Normal - 1]; // Wavefront uses 1-based indexing :(
                    ObjVector3 normal_1 = file.VertexNormals[f.Vertices[1].Normal - 1];
                    ObjVector3 normal_2 = file.VertexNormals[f.Vertices[2].Normal - 1];

                    normals.Add(new Vector3(normal_0.X, normal_0.Y, normal_0.Z));
                    normals.Add(new Vector3(normal_1.X, normal_1.Y, normal_1.Z));
                    normals.Add(new Vector3(normal_2.X, normal_2.Y, normal_2.Z));

                    ObjVector3 textureVertex_0 = file.TextureVertices[f.Vertices[0].Texture - 1];
                    ObjVector3 textureVertex_1 = file.TextureVertices[f.Vertices[1].Texture - 1];
                    ObjVector3 textureVertex_2 = file.TextureVertices[f.Vertices[2].Texture - 1];

                    uvs.Add(new UV() { u = textureVertex_0.X, v = textureVertex_0.Y });
                    uvs.Add(new UV() { u = textureVertex_1.X, v = textureVertex_1.Y });
                    uvs.Add(new UV() { u = textureVertex_2.X, v = textureVertex_2.Y });

                    uvMappings.Add(new UvMapping()
                    {
                        mapping_0 = (short)(vertexCount + 0),
                        mapping_1 = (short)(vertexCount + 1),
                        mapping_2 = (short)(vertexCount + 2),
                    });

                    triangles.Add(new Triangle()
                    {
                        v0 = (ushort)(f.Vertices[0].Vertex - 1),
                        v1 = (ushort)(f.Vertices[1].Vertex - 1),
                        v2 = (ushort)(f.Vertices[2].Vertex - 1)
                    });

                    vertexCount += 3;

                    ri.Log($"MTL NAME {f.MaterialName}");
                    Texture tex = textures.FirstOrDefault(t => t.Name == f.MaterialName);
                    if (tex != null)
                    {
                        vis.off_texture = tex.TexData;
                    }
                }

                if (!Memory.IsNull(newMtl.collideMaterial) || true)
                {
                    CollideMaterial cm = new CollideMaterial();
                    string[] nameSplit = objGroup.Name.Split(new[] { ':' }, 2);
                    if (nameSplit.Length > 1)
                    {
                        string flagStr = nameSplit[1];
                        if (!string.IsNullOrEmpty(flagStr))
                        {
                            ushort flag = ushort.Parse(flagStr);
                            cm.identifier = (CollisionFlags)flag;
                        }
                        ri.Log($"OBJ {objGroup.Name} FLAGS {cm.identifier:F}");
                    }
                    else
                    {
                        cm.identifier = CollisionFlags.GrabbableLedge;
                    }
                    newMtl.collideMaterial = cm.ToUnmanaged();
                }

                newMtl.visualMaterial = vis.ToUnmanaged();
                geoElem.material = newMtl.ToUnmanaged();

                Vector3[] normalArray = normals.ToArray();
                Triangle[] triArray = triangles.ToArray();
                UV[] uvArray = uvs.ToArray();

                geoElem.SetTriangles(triArray);
                geoElem.SetUVS(uvArray);
                geoElem.SetUVMappings(uvMappings.ToArray());
                geoElem.SetNormals(normalArray);

                // Group end

                elements[i] = geoElem.ToUnmanaged();
            }

            IEnumerable<Vector3> verts = file.Vertices.Select(v =>
            {
                return new Vector3(v.Position.X, v.Position.Y, v.Position.Z);
            });

            Vector3[] vertsArray = verts.ToArray();

            go->SetVertices(vertsArray);
            go->SetNormals(new Vector3[vertsArray.Length]); // empty normals for geometric object

            GeometricObject.ElementType[] types = new GeometricObject.ElementType[elements.Length];

            int[] tempArray = new int[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                types[i] = GeometricObject.ElementType.Triangles;
                tempArray[i] = (int)elements[i];
            }
            go->SetGeometricElementTypes(types);
            go->SetGeometricElements(tempArray);

            return go;
        }
    }
}