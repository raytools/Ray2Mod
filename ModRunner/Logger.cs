using System;
using System.Threading;
using Ray2Mod;

namespace ModRunner
{
    public class Logger : RemoteInterface
    {
        public override void Injected(int pid)
        {
            Console.WriteLine($"DLL injected, PID: {pid}");
        }

        public override void Log(string msgPacket)
        {
            Console.WriteLine(msgPacket);
        }

        public override void HandleError(Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        public override void GameClosed()
        {
            // lazy hack to make sure IPC channel is closed
            new Thread(() =>
            {
                Thread.Sleep(500);
                System.Environment.Exit(0);
            }).Start();
        }
    }
}