using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(Vector3 o) : this(o.X, o.Y, o.Z) { }

        public float X;
        public float Y;
        public float Z;

        public static Vector3 Zero = new Vector3(0, 0, 0);
        
        public double Length => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        public static Vector3 operator+ (Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator * (Vector3 a, float magnitude)
        {
            return new Vector3(a.X * magnitude, a.Y * magnitude, a.Z * magnitude);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static bool operator == (Vector3 a, Vector3 b)
        {
            return (a.X == b.X && a.Y == b.Y && a.Z == b.Z);
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a==b);
        }

        public Vector3 Normalized()
        {
            float length = (float)Length;
            return new Vector3(X / length, Y / length, Z / length);
        }

        public override string ToString() => $"{X:F3}, {Y:F3}, {Z:F3}";
    }
}