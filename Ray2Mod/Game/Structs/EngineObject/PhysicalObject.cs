using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.Geometry;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalObject
    {
        public VisualSet* visualSet;
        public CollideSet* collideSet;
        public int* boundingVolume;
    }
}