using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ray2Mod.Types;

namespace Ray2Mod.Utils
{
    public static class TextureLoader
    {
        public static List<Texture> GetTextures()
        {
            IntPtr tPtr = (IntPtr)0x502680;
            IntPtr tMemChannelsPtr = (IntPtr)0x501660;

            uint[] tMemChannels = new uint[1024];
            for (int i = 0; i < tMemChannels.Length; i++)
            {
                tMemChannels[i] = (uint) Marshal.ReadInt32(tMemChannelsPtr + 4 * i);
            }

            List<Texture> textures = new List<Texture>();
            for (int i = 0; i < tMemChannels.Length; i++)
            {
                IntPtr textureStructPtr = Marshal.ReadIntPtr(tPtr + 4 * i);
                if (textureStructPtr != IntPtr.Zero && tMemChannels[i] != 0xC0DE0005)
                {
                    Texture texture = new Texture(textureStructPtr);
                    textures.Add(texture);
                }
            }

            return textures;
        }
    }
}