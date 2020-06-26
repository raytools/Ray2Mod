using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CollideSet
    {
        public int collisionZoneMechanics;
        public int collisionZoneDetection;
        public int collisionZoneEvent;
        public GeometricObject* collisionZoneReact;

    }
}
