using System.Globalization;

using Ray2Mod.Components.Types;
using Ray2Mod.Game;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs;

namespace Ray2Mod.Utils
{
    public static class TextUtils
    {
        #region Formatting

        public static string Red(this string s)
        {
            return "/O200:" + s + "/O0:";
        }

        public static string Yellow(this string s)
        {
            return "/O400:" + s + "/O0:";
        }

        public static string Special(this string s)
        {
            return "/O800:" + s + "/O0:";
        }

        public static string Center(this string s)
        {
            return "/c:" + s;
        }

        public static string NL(this string s)
        {
            return s + "/l:";
        }

        public static string Arrow = "\\";

        public static string D3(this float f)
        {
            return f.ToString("0.000", CultureInfo.InvariantCulture);
        }

        public static string D3(this double d)
        {
            return d.ToString("0.000", CultureInfo.InvariantCulture);
        }

        public static string KeyValue(this string k, string v)
        {
            return k.Red() + Arrow + v;
        }

        #endregion

        //TODO: phase these out, use TextOverlay class instead?
        internal static void TextOverlay(string text, float size, float x, float y, byte alpha = 255)
        {
            Text2D textStruct = new Text2D
            {
                text = text,
                size = size,
                positionX = x,
                positionY = y,
                alphaByte = alpha
            };

            TextOverlay(textStruct);
        }

        internal static void TextOverlay(Text2D textStruct)
        {
            using (StructPtr textPtr = new StructPtr(textStruct))
            {
                TextFunctions.DrawText.Call(Offsets.GlobalGraphicsContext, textPtr);
            }
        }
    }
}