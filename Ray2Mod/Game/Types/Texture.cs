using System;
using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs;
using Ray2Mod.Utils;

namespace Ray2Mod.Game.Types
{
    public unsafe class Texture
    {
        public Texture(int ptr) : this((IntPtr)ptr) { }

        public Texture(IntPtr ptr)
        {
            Name = Marshal.PtrToStringAnsi(ptr + 0x46);
            TexData = (TextureData*)ptr;

            byte[] ptrBytes = Memory.GetBytes(ptr - 0x8, 4);

            if (ptrBytes[0] == 0 && ptrBytes[1] == 0)
            {
                if (ptrBytes[2] == 0)
                {
                    Pointer = Marshal.ReadIntPtr(ptr - 0x90);
                }
                else
                {
                    Pointer = Marshal.ReadIntPtr(ptr - ptrBytes[2] - 0x8);
                }
            }
            else
            {
                Pointer = IntPtr.Zero;
                Animated = true;
            }
        }

        public string Name { get; }
        public IntPtr Pointer { get; }
        public TextureData* TexData { get; }
        public bool Animated { get; }
    }
}