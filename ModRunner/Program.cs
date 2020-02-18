using System;

namespace ModRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Logger logger = new Logger();

                HookManager manager = new HookManager(args, logger, "Rayman2", "Rayman2.exe", "Rayman2.exe.noshim");
                manager.Inject();

                while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
            }
        }
    }
}
