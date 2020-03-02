using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix
    {
        public int transformationType;
        public Vector3 position;
        public float scaleX;
        public float skewY1;
        public float skewZ1;
        public float skewX1;
        public float scaleY;
        public float skewZ2;
        public float skewX2;
        public float skewY2;
        public float scaleZ;
        public float t4_scaleX;
        public float t4_skewY1;
        public float t4_skewZ1;
        public float t4_skewX1;
        public float t4_scaleY;
        public float t4_skewZ2;
        public float t4_skewX2;
        public float t4_skewY2;
    }
}