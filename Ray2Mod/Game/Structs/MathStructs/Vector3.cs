using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.MathStructs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(Vector3 o) : this(o.x, o.y, o.z)
        {
        }

        public void Set(Vector3 newVec)
        {
            this.x = newVec.x;
            this.y = newVec.y;
            this.z = newVec.z;
        }

        public float x;
        public float y;
        public float z;

        public static Vector3 Zero = new Vector3(0, 0, 0);

        public double Length => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

        /// <summary>
        /// Returns true if all components of this vector are a floating point number that's not NaN or an infinity.
        /// </summary>
        /// <returns>true for vectors with valid components, false otherwise</returns>
        public bool IsValid()
        {
            return !(float.IsNaN(x) || float.IsNaN(y) || float.IsNaN(z) || float.IsInfinity(x) || float.IsInfinity(y) || float.IsInfinity(z));
        }

        public static Vector3 operator *(Vector3 a, float magnitude) => new Vector3(a.x * magnitude, a.y * magnitude, a.z * magnitude);

        public static Vector3 operator *(Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);

        public static bool operator ==(Vector3 a, Vector3 b) => (a.x == b.x && a.y == b.y && a.z == b.z);

        public static bool operator !=(Vector3 a, Vector3 b) => !(a == b);

        public Vector3 Cross(Vector3 b)
        {
            return new Vector3(y * b.z - z * b.y, z * b.x - x * b.z, x * b.y - y * b.x);
        }

        public Vector3 Normalized()
        {
            float length = (float)Length;
            return new Vector3(x / length, y / length, z / length);
        }

        public override string ToString()
        {
            return $"({x:F3}, {y:F3}, {z:F3})";
        }
    }
}