using Ray2Mod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Material {

    [StructLayout(LayoutKind.Sequential)]
    public struct CollisionFlags {

        public Flags flags;

        [Flags]
        public enum Flags : ushort{
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

        public bool HasFlag(Flags flag)
        {
            return (flags & flag) == flag;
        }

        public void SetFlag(Flags flag, bool set)
        {
            if (set) {
                flags = flags | flag;
            } else {
                flags = flags & ~flag;
            }
        }
    }
}
