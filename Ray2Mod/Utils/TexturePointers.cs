using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Utils
{
    public static class TexturePointers
    {
        // TODO: Load textures dynamically from the game

        public static int sparkTexture = Marshal.ReadInt32(Memory.GetPointerAtOffset((IntPtr)0x502810, 0x120));
        public static int blueSparkTexture = Marshal.ReadInt32(Memory.GetPointerAtOffset((IntPtr)0x502750, 0x2BC));

        public static int lumIcon = Marshal.ReadInt32(Memory.GetPointerAtOffset((IntPtr)0x502790, 0xCC));
        public static int cageIcon = Marshal.ReadInt32(Memory.GetPointerAtOffset((IntPtr)0x502790, 0x400));
        public static int rayIcon = Marshal.ReadInt32(Memory.GetPointerAtOffset((IntPtr)0x5027D0, 0x228));

    }
}