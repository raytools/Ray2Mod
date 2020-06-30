using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.SPO
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SuperObjectFlags
    {
        public ushort rawFlags;

        public EnumSuperObjectFlags Flags
        {
            get
            {
                return (EnumSuperObjectFlags)rawFlags;
            }
            set
            {
                rawFlags = (ushort)value;
            }
        }

        [Flags]
        public enum EnumSuperObjectFlags : int
        {
            NoCollision = 1 << 0,
            Invisible = 1 << 1,
            NoTransformMatrix = 1 << 2, // No scale, no rotation
            ZoomInsteadOfScale = 1 << 3, // Same scale coëfficient over all 3 axes
            BoundingBoxInsteadOfSphere = 1 << 4,
            DisplayOnTop = 1 << 5, // displayed over all C - 0 ; non collisionnable. Superimposed
            NoRayTracing = 1 << 6,
            NoShadow = 1 << 7,
            SemiLookat = 1 << 8,
            SpecialBoundingVolumes = 1 << 9, // Super object has one or more children whose bounding volumes are not included in that of the father
            Flag10 = 1 << 10,
            Flag11 = 1 << 11,
            Flag12 = 1 << 12,
            Flag13 = 1 << 13,
            Flag14 = 1 << 14,
            InfluencedByMagnet = 1 << 15,
            Transparent = 1 << 16,
            NoLighting = 1 << 17,
            SuperimposedClipping = 1 << 18,
            OutlineMode = 1 << 19,
            Flag20 = 1 << 20,
            Flag21 = 1 << 21,
            Flag22 = 1 << 22,
            Flag23 = 1 << 23,
            Flag24 = 1 << 24,
            Flag25 = 1 << 25,
            Flag26 = 1 << 26,
            Flag27 = 1 << 27,
            Flag28 = 1 << 28,
            Flag29 = 1 << 29,
            Flag30 = 1 << 30,
            Flag31 = 1 << 31
        }

        public bool HasFlag(EnumSuperObjectFlags flag)
        {
            return (Flags & flag) == flag;
        }

        public void SetFlag(EnumSuperObjectFlags flag, bool set)
        {
            if (set)
            {
                Flags = Flags | flag;
            }
            else
            {
                Flags = Flags & ~flag;
            }
        }
    }
}