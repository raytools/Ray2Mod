using System.Collections.Generic;

using Ray2Mod.Game;
using Ray2Mod.Game.Types;

namespace Ray2Mod.Utils
{
    //TODO: do something, anything, about this. it can't just be an util
    public static unsafe class TextureLoader
    {
        public static List<Texture> GetTextures()
        {
            int* tPtr = (int*)Offsets.TexturePointer;
            int* tMemChannelsPtr = (int*)Offsets.MemChannelsPointer;

            uint[] tMemChannels = new uint[1024];
            for (int i = 0; i < tMemChannels.Length; i++)
            {
                tMemChannels[i] = (uint)*(tMemChannelsPtr + i);
            }

            List<Texture> textures = new List<Texture>();
            for (int i = 0; i < tMemChannels.Length; i++)
            {
                int textureStructPtr = *(tPtr + i);
                if (textureStructPtr != 0 && tMemChannels[i] != 0xC0DE0005)
                {
                    Texture texture = new Texture(textureStructPtr);
                    textures.Add(texture);
                }
            }

            return textures;
        }
    }
}