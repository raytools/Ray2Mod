using Ray2Mod.Components.Types;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs;
using System.Globalization;

namespace Ray2Mod.Utils
{
    public static class TextUtils
    {
        #region Formatting

        public static string Red(this string s) => "/O200:" + s + "/O0:";
        public static string Yellow(this string s) => "/O400:" + s + "/O0:";
        public static string Special(this string s) => "/O800:" + s + "/O0:";

        public static string Center(this string s) => "/c:" + s;
        public static string NL(this string s) => s + "/l:";
        public static string Arrow = "\\";

        public static string D3(this float f) => f.ToString("0.000", CultureInfo.InvariantCulture);
        public static string D3(this double d) => d.ToString("0.000", CultureInfo.InvariantCulture);
        public static string KeyValue(this string k, string v) => k.Red() + Arrow + v;

        #endregion

        public static void TextOverlay(string text, float size, float x, float y, byte alpha = 255)
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

        public static void TextOverlay(Text2D textStruct)
        {
            using (StructPtr textPtr = new StructPtr(textStruct))
            {
                TextFunctions.DrawText.Call(0x5004D4, textPtr);
            }
        }
    }
}