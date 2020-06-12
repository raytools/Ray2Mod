using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GeometricObject {
        public int* off_vertices;
        public int* off_normals;

    }
}