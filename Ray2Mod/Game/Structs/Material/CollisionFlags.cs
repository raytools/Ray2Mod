using System;

namespace Ray2Mod.Game.Structs.Material
{
    [Flags]
    public enum CollisionFlags : ushort
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
}