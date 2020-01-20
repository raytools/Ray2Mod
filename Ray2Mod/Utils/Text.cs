using System.Globalization;

namespace Ray2Mod.Utils
{
    public static class Text
    {
        public static string Red(this string s) => "/O200:" + s + "/O0:";
        public static string Yellow(this string s) => "/O400:" + s + "/O0:";

        public static string NL(this string s) => s + "/l:";
        public static string Arrow = "\\";

        public static string D3(this float f) => f.ToString("0.000", CultureInfo.InvariantCulture);
        public static string D3(this double d) => d.ToString("0.000", CultureInfo.InvariantCulture);
        public static string KeyValue(this string k, string v) => k.Red() + Arrow + v;
    }
}