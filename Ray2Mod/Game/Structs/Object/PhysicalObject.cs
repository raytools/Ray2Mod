using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs {
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalObject {
        public VisualSet* visualSet;
        public int* collideSet;
        public int* boundingVolume;
    }
}
