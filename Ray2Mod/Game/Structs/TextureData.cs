using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TextureData
    {
        public uint field0;
        public ushort field4;
        public ushort field6;
        public IntPtr tempBuffer;
        public uint fieldC;
        public uint field10;
        public uint flags;
        public ushort height_;
        public ushort width_;
        public ushort height;
        public ushort width;
        public uint currentScrollX;
        public uint currentScrollY;
        public uint textureScrollingEnabled;
        public uint alphaMask;
        public uint field30;
        public uint field34;
        public uint field38;
        public uint field3C;
        public uint field40;
        public byte field44;
        public byte flagsByte;
    }
}