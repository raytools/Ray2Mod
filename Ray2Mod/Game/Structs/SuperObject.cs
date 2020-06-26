using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Game.Structs.LinkedLists;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SuperObject
    {
        public SuperObjectType type;
        public int* engineObjectPtr;

        //linked list
        public LinkedList.SuperObjectList children;

        public SuperObject* nextBrother;
        public SuperObject* previousBrother;
        public SuperObject* parent;
        public Matrix* matrixPtr;
        public Matrix* matrixPtr2;

        public int globalMatrix;
        public int drawFlags;
        public SuperObjectFlags flags;
        public int* boundingVolume;

        public Perso* PersoData
        {
            get
            {
                AssertType(SuperObjectType.Perso);
                return (Perso*)engineObjectPtr;
            }

            set
            {
                AssertType(SuperObjectType.Perso);
                engineObjectPtr = (int*)value;
            }
        }

        public Sector* SectorData
        {
            get
            {
                AssertType(SuperObjectType.Sector);
                return (Sector*)engineObjectPtr;
            }

            set
            {
                AssertType(SuperObjectType.Sector);
                engineObjectPtr = (int*)value;
            }
        }

        public PhysicalObject* PhysicalObjectData
        {
            get
            {
                AssertType(SuperObjectType.PhysicalObject);
                return (PhysicalObject*)engineObjectPtr;
            }

            set
            {
                AssertType(SuperObjectType.PhysicalObject);
                engineObjectPtr = (int*)value;
            }
        }

        public IPO* IPOData
        {
            get
            {
                AssertType(SuperObjectType.IPO);
                return (IPO*)engineObjectPtr;
            }

            set
            {
                AssertType(SuperObjectType.IPO);
                engineObjectPtr = (int*)value;
            }
        }

        private void AssertType(SuperObjectType wantedType)
        {
            if (type != wantedType)
            {
                throw new InvalidCastException($"Trying to cast SuperObject of type {type} to {wantedType}");
            }
        }

        public BoundingVolumeBox* BoundingVolumeAsBox
        {
            get
            {
                if (flags.HasFlag(SuperObjectFlags.EnumSuperObjectFlags.BoundingBoxInsteadOfSphere))
                {
                    return (BoundingVolumeBox*)boundingVolume;
                }
                else
                {
                    throw new InvalidCastException($"Bounding volume is a sphere, unless flag BoundingBoxInsteadOfSphere is set");
                }
            }
        }

        public BoundingVolumeSphere* BoundingVolumeAsSphere
        {
            get
            {
                if (!flags.HasFlag(SuperObjectFlags.EnumSuperObjectFlags.BoundingBoxInsteadOfSphere))
                {
                    return (BoundingVolumeSphere*)boundingVolume;
                }
                else
                {
                    throw new InvalidCastException($"Bounding volume is a box instead of a sphere, because flag BoundingBoxInsteadOfSphere is set");
                }
            }
        }
    }

    public enum SuperObjectType
    {
        Unknown = 0x0,
        World = 0x1,
        Perso = 0x2,
        Sector = 0x4,
        PhysicalObject = 0x8,
        IPO = 0x20,
        IPO_2 = 0x40,
        GeometricObject = 0x400, // Geometric Object
        GeometricShadowObject = 0x80000, // Instantiated Geometric Object
    }
}