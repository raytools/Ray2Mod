using JeremyAnsel.Media.WavefrontObj;
using Ray2Mod.Game.Structs.Material;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Types;
using Ray2Mod.Utils;
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

            //GeometricElementTriangles* gt = (GeometricElementTriangles*)go->off_elements[0];// new GeometricElementTriangles().ToUnmanaged();

            GameMaterial mtl = *material;
            //VisualMaterial vm = *mtl.visualMaterial;

            List<Texture> textures = TextureLoader.GetTextures();

            foreach (Texture t in textures)
            {
                ri.Log($"Texture ptr 0x{(int)t.TexData:X} {t.Name}");
            }

            /*
            Texture tex = textures.First(t => t.Name == @"textures_objets\divers\cam_nocam_nz.tga");
            if (tex != null)
            {
                ri.Log($"PREVIOUS TEX PTR 0x{(int)vm.off_texture:X}");
                ri.Log($"REPLACING WITH TEX PTR 0x{(int)tex.TexData:X}");
                vm.off_texture = tex.TexData;
            }

            ri.Log($"MATERIAL CHANGED NEW PTR 0x{(int)gt->material:X}, VIS 0x{(int)(gt->material->visualMaterial):X}, TEX 0x{(int)(gt->material->visualMaterial->off_texture):X}");
            */

            var file = ObjFile.FromFile(objPath);

            GeometricElementTriangles*[] elements = new GeometricElementTriangles*[file.Groups.Count];

            for (int i = 0; i < file.Groups.Count; i++)
            {
                ObjGroup objGroup = file.Groups[i];
                GeometricElementTriangles geoElem = new GeometricElementTriangles();

                ri.Log($"OBJECT GROUP {objGroup.Name}");

                GameMaterial newMtl = mtl;
                VisualMaterial vis = *newMtl.visualMaterial;

                // Group start

                // We want the normals to have the same indices as the vertices, so generate a new list.
                List<Vector3> normals = new List<Vector3>();
                List<Triangle> triangles = new List<Triangle>();
                List<UV> uvs = new List<UV>();
                List<UvMapping> uvMappings = new List<UvMapping>();

                short vertexCount = 0;
                foreach (var f in objGroup.Faces)
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

                if (!Memory.IsNull(newMtl.collideMaterial))
                {
                    CollideMaterial cm = *newMtl.collideMaterial;
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

                var normalArray = normals.ToArray();
                var triArray = triangles.ToArray();
                var uvArray = uvs.ToArray();

                geoElem.SetTriangles(triArray);
                geoElem.SetUVS(uvArray);
                geoElem.SetUVMappings(uvMappings.ToArray());
                geoElem.SetNormals(normalArray);

                // Group end

                elements[i] = geoElem.ToUnmanaged();
            }

            var verts = file.Vertices.Select(v =>
            {
                return new Vector3(v.Position.X, v.Position.Y, v.Position.Z);
            });

            var vertsArray = verts.ToArray();

            go->SetVertices(vertsArray);
            go->SetNormals(new Vector3[vertsArray.Length]); // empty normals for geometric object

            var types = new GeometricObject.ElementType[elements.Length];

            int[] tempArray = new int[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                types[i] = GeometricObject.ElementType.Triangles;
                tempArray[i] = (int)elements[i];
            }
            go->SetGeometricElementTypes(types);
            go->SetGeometricElements(tempArray);

            //go->SetGeometricElements(new []{ (int)gt });

            return go;
        }
    }
}