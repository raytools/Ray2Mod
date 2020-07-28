using System;
using System.Globalization;

using Ray2Mod;

namespace ModRunner
{
    public class Logger : RemoteInterface
    {
        public override void Injected(int pid, string modName)
        {
            Log($"{modName} injected, PID: {pid}");
        }

        public override void Log(string msgPacket, LogType type = LogType.Info)
        {
            string message = $"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} [{type}] {msgPacket}";
            Console.WriteLine(message);
        }

        public override void HandleError(Exception e)
        {
            Log(e.ToString(), LogType.Error);
        }

        public override void ProcessExit()
        {
            Environment.Exit(0);
        }
    }
}