using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4
    {

        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector4 o) : this(o.x, o.y, o.z, o.w) { }

        public void Set(Vector4 newVec)
        {
            this.x = newVec.x;
            this.y = newVec.y;
            this.z = newVec.z;
            this.w = newVec.w;
        }

        public static Vector4 Zero = new Vector4(0, 0, 0, 0);

        public double Length => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2) + Math.Pow(w, 2));
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4 operator *(Vector4 a, float magnitude)
        {
            return new Vector4(a.x * magnitude, a.y * magnitude, a.z * magnitude, a.w * magnitude);
        }

        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return (a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w);
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !(a == b);
        }

        public Vector4 Normalized()
        {
            float length = (float)Length;
            return new Vector4(x / length, y / length, z / length, w / length);
        }

        public override string ToString() => $"({x:F3}, {y:F3}, {z:F3}, {w:F3})";
    }
}