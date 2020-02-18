using System;
using System.Runtime.InteropServices;
using Ray2Mod.Game.Structs;

namespace Ray2Mod.Game.Types
{
    public class Texture
    {
        public Texture(IntPtr ptr)
        {
            Name = Marshal.PtrToStringAnsi(ptr + 0x46);
            Pointer = Marshal.ReadIntPtr(ptr - 0x90);
            Data = Marshal.PtrToStructure<TextureData>(ptr);
        }

        public string Name { get; }
        public IntPtr Pointer { get; }
        public TextureData Data { get; }
    }
}