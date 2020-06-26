using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Geometry
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BoundingVolumeBox
    {
        public Vector3 boxMin;
        public Vector3 boxMax;

        public Vector3 BoxSize
        {
            get
            {
                return boxMax - boxMin; ;
            }
        }

        public Vector3 BoxCenter
        {
            get
            {
                return boxMin + (BoxSize * 0.5f);
            }
        }

        /// <summary>
        /// Combines the bounding box with another bounding box, making sure that both bounding boxes are contained in the new one
        /// </summary>
        /// <param name="other">The bounding box to combine with</param>
        /// <returns>A new bounding box that contains both bounding boxes</returns>
        public BoundingVolumeBox Combine(BoundingVolumeBox other)
        {
            if (!(boxMin.IsValid() && boxMax.IsValid()))
            {
                return other;
            }

            if (!(other.boxMin.IsValid() && other.boxMax.IsValid()))
            {
                return this;
            }

            float minX = Math.Min(boxMin.x, other.boxMin.x);
            float minY = Math.Min(boxMin.y, other.boxMin.y);
            float minZ = Math.Min(boxMin.z, other.boxMin.z);

            float maxX = Math.Max(boxMax.x, other.boxMax.x);
            float maxY = Math.Max(boxMax.y, other.boxMax.y);
            float maxZ = Math.Max(boxMax.z, other.boxMax.z);

            return new BoundingVolumeBox()
            {
                boxMin = new Vector3(minX, minY, minZ),
                boxMax = new Vector3(maxX, maxY, maxZ),
            };
        }

        public void Set(BoundingVolumeBox newBox)
        {
            this.boxMin = newBox.boxMin;
            this.boxMax = newBox.boxMax;
        }

        public override string ToString()
        {
            return $"BoundingVolumeBox from {boxMin} to {boxMax}";
        }

        /// <summary>
        /// Generate a bounding sphere that contains this bounding box
        /// </summary>
        /// <returns>A bounding sphere that contains this bounding box</returns>
        public BoundingVolumeSphere CreateBoundingSphere()
        {
            float radius = (float)(BoxSize * 0.5f).Length;

            return new BoundingVolumeSphere()
            {
                center = BoxCenter,
                radius = radius
            };
        }
    }

    public unsafe struct BoundingVolumeSphere
    {
        public Vector3 center;
        public float radius;
    }
}
