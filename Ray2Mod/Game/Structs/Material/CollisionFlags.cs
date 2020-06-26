using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Material
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CollisionFlags
    {
        public ushort rawFlags;

        public EnumCollisionFlags Flags
        {
            get
            {
                return (EnumCollisionFlags)rawFlags;
            }
            set
            {
                rawFlags = (ushort)value;
            }
        }

        public enum EnumCollisionFlags : ushort
        {
            None = 0,
            Slide = 1 << 0,
            Trampoline = 1 << 1,
            GrabbableLedge = 1 << 2,
            Wall = 1 << 3,
            FlagUnknown = 1 << 4,
            HangableCeiling = 1 << 5,
            ClimbableWall = 1 << 6,
            Electric = 1 << 7,
            LavaDeathWarp = 1 << 8,
            FallTrigger = 1 << 9,
            HurtTrigger = 1 << 10,
            DeathWarp = 1 << 11,
            FlagUnk2 = 1 << 12,
            FlagUnk3 = 1 << 13,
            Water = 1 << 14,
            NoCollision = 1 << 15,
            All = 0xFFFF
        }

        public bool HasFlag(EnumCollisionFlags flag)
        {
            return (Flags & flag) == flag;
        }

        public void SetFlag(EnumCollisionFlags flag, bool set)
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