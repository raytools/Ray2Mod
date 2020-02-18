using System;
using Ray2Mod;

namespace ModRunner
{
    public class Logger : RemoteInterface
    {
        public override void Injected(int pid, string modName)
        {
            Console.WriteLine($"{modName} injected, PID: {pid}");
        }

        public override void Log(string msgPacket, uint id = 0)
        {
            Console.WriteLine(msgPacket);
        }

        public override void HandleError(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}