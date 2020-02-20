using Ray2Mod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ModLoader
{
    public class Loader
    {
        public Loader(RemoteInterface remoteInterface)
        {
            Interface = remoteInterface;
        }

        public RemoteInterface Interface { get; }

        public List<IMod> Mods { get; private set; }

        public void LoadMods(string[] modNames)
        {
            Mods = new List<IMod>();

            Interface.Log("Loading DLLs...");
            foreach (string name in modNames)
            {
                string shortName = Path.GetFileName(name);

                Interface.Log($"Loading {shortName}...", LogType.Debug);
                
                if (LoadDll(name, out IMod mod)) Mods.Add(mod);
                else Interface.Log($"An error occurred. Skipping {shortName}...", LogType.Warning);
            }
        }

        private bool LoadDll<T>(string path, out T mod)
        {
            try
            {
                Assembly dll = Assembly.LoadFile(path);
                Type dllType = dll.GetExportedTypes().First(t => typeof(T).IsAssignableFrom(t));

                mod = (T)Activator.CreateInstance(dllType);

                return true;
            }
            catch (Exception e)
            {
                Interface.Log(e.ToString(), LogType.Error);
            }

            mod = default;
            return false;
        }
    }
}