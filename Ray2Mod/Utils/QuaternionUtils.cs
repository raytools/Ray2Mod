using System;
using System.Numerics;

namespace Ray2Mod.Utils
{
    public static class QuaternionUtils
    {
        public static Vector3 ToEuler(this Quaternion q)
        {
            Vector3 pitchYawRoll;

            double sqw = q.W * q.W;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;

            pitchYawRoll.Y = (float)Math.Atan2(2f * q.X * q.W + 2f * q.Y * q.Z, 1 - 2f * (sqz + sqw));     // Yaw 
            pitchYawRoll.X = (float)Math.Asin(2f * (q.X * q.Z - q.W * q.Y));                             // Pitch 
            pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.Y + 2f * q.Z * q.W, 1 - 2f * (sqy + sqz));      // Roll 
            return pitchYawRoll;
        }
    }
}
