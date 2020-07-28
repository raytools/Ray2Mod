using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string[] dllsToLoad = FixPaths(args).ToArray();

                Logger logger = new Logger();
                HookManager manager = new HookManager("ModLoader.dll", logger, "Rayman2", "Rayman2.exe", "Rayman2.exe.noshim");
                manager.Inject(dllsToLoad);

                while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
            }
        }

        static IEnumerable<string> FixPaths(params string[] paths)
        {
            foreach (string path in paths)
            {
                if (Path.IsPathRooted(path))
                {
                    yield return path;
                }
                else
                {
                    yield return Path.Combine(Directory.GetCurrentDirectory(), path);
                }
            }
        }
    }
}
