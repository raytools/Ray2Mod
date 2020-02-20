using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ray2Mod.Utils
{
    public static class OtherUtils
    {
        public static string GetVersionString(Assembly assembly)
        {
            Version assemblyVersion = assembly.GetName().Version;
            string version = $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";

            return version;
        }

        public static string GetAssemblyVersion() => GetVersionString(Assembly.GetCallingAssembly());

        public static string GetAssemblyProductName() => Assembly.GetCallingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;

        public static Dictionary<string, Dictionary<string, string>> Levels = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "Woods of Light", new Dictionary<string, string>
                {
                    { "Jail", "jail_20" },
                    { "Woods of Light", "learn_10" }
                }
            },
            {
                "Fairy Glade", new Dictionary<string, string>
                {
                    { "Section 1", "learn_30" },
                    { "Section 2", "learn_31" },
                    { "Section 3", "bast_20" },
                    { "Section 4", "bast_22" },
                    { "Section 5", "learn_60" },
                }
            },
            {
                "Marshes of Awakening", new Dictionary<string, string>
                {
                    { "Section 1", "ski_10" },
                    { "Section 2", "ski_60" },
                }
            },
            {
                "Bayou", new Dictionary<string, string>
                {
                    { "Section 1", "chase_10" },
                    { "Section 2", "chase_22" },
                }
            },
            {
                "Sanctuary of Water and Ice", new Dictionary<string, string>
                {
                    { "Section 1", "water_10" },
                    { "Section 2", "water_20" },
                }
            },
            {
                "Menhir Hills", new Dictionary<string, string>
                {
                    { "Section 1", "rodeo_10" },
                    { "Section 2", "rodeo_40" },
                    { "Section 3", "rodeo_60" },
                }
            },
            {
                "Cave of Bad Dreams", new Dictionary<string, string>
                {
                    { "Section 1", "vulca_10" },
                    { "Section 2", "vulca_20" },
                }
            },
            {
                "Canopy", new Dictionary<string, string>
                {
                    { "Section 1", "glob_30" },
                    { "Section 2", "glob_10" },
                    { "Section 3", "glob_20" },
                }
            },
            {
                "Whale Bay", new Dictionary<string, string>
                {
                    { "Section 1", "whale_00" },
                    { "Section 2", "whale_05" },
                    { "Section 3", "whale_10" },
                }
            },
            {
                "Sanctuary of Stone and Fire", new Dictionary<string, string>
                {
                    { "Section 1", "plum_00" },
                    { "Section 2", "plum_20" },
                    { "Section 3", "plum_10" },
                }
            },
            {
                "Echoing Caves", new Dictionary<string, string>
                {
                    { "Section 1", "bast_10" },
                    { "Section 2", "cask_10" },
                    { "Section 3", "cask_30" },
                }
            },
            {
                "Precipice", new Dictionary<string, string>
                {
                    { "Section 1", "nave_10" },
                    { "Section 2", "nave_15" },
                    { "Section 3", "nave_20" },
                }
            },
            {
                "Top of the World", new Dictionary<string, string>
                {
                    { "Section 1", "seat_10" },
                    { "Section 2", "seat_11" },
                }
            },
            {
                "Sanctuary of Rock and Lava", new Dictionary<string, string>
                {
                    { "Section 1", "earth_10" },
                    { "Section 2", "earth_20" },
                    { "Section 3", "earth_30" },
                }
            },
            {
                "Beneath the Sanctuary of RnL", new Dictionary<string, string>
                {
                    { "Section 1", "helic_10" },
                    { "Section 2", "helic_20" },
                    { "Section 3", "helic_30" },
                }
            },
            {
                "Tomb of the Ancients", new Dictionary<string, string>
                {
                    { "Section 1", "morb_00" },
                    { "Section 2", "morb_10" },
                    { "Section 3", "morb_20" },
                }
            },
            {
                "Iron Mountains", new Dictionary<string, string>
                {
                    { "Section 1", "learn_40" },
                    { "Section 2", "ile_10" },
                    { "Section 3", "mine_10" },
                }
            },
            {
                "Prison Ship", new Dictionary<string, string>
                {
                    { "Section 1", "boat01" },
                    { "Section 2", "boat02" },
                    { "Section 3", "astro_00" },
                    { "Section 4", "astro_10" },
                }
            },
            {
                "The Crow's Nest", new Dictionary<string, string>
                {
                    { "Section 1", "rhop_10" },
                }
            },

        };
    }
}