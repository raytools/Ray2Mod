using EasyHook;
using Ray2Mod;
using Ray2Mod.Utils;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ModLoader
{
    public class EntryPoint : IEntryPoint
    {
        public EntryPoint(RemoteHooking.IContext inContext, string channelName, string[] dllsToLoad)
        {
            Interface = RemoteHooking.IpcConnectClient<RemoteInterface>(channelName);
            Interface.Injected(RemoteHooking.GetCurrentProcessId(), GetType().Assembly.GetName().Name);
        }

        public RemoteInterface Interface { get; }
        private Loader Loader { get; set; }

        public void Run(RemoteHooking.IContext inContext, string inChannelName, string[] dllsToLoad)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Interface.Log($"== {OtherUtils.GetAssemblyProductName()} v{OtherUtils.GetAssemblyVersion()} ==");

                Loader = new Loader(Interface);
                Loader.LoadMods(dllsToLoad);

                // Make sure the game has started loading a level
                while (Marshal.ReadByte((IntPtr)0x500380) < 5) { }

                Interface.Log("Initializing mods...");
                foreach (IMod mod in Loader.Mods)
                {
                    Interface.Log(mod.GetType().FullName, LogType.Debug);
                    mod.Initialize(Interface);
                }

                // Make sure the game is fully loaded
                while (Marshal.ReadByte((IntPtr)0x500380) < 9) { }

                Interface.Log("Starting mods...");
                foreach (IMod mod in Loader.Mods)
                {
                    Interface.Log($"{mod.GetType().Assembly.GetName().Name} v{OtherUtils.GetVersionString(mod.GetType().Assembly)}", LogType.Debug);
                    mod.Run();
                }

                stopwatch.Stop();
                Interface.Log($"Done. ({stopwatch.Elapsed.TotalSeconds:F3}s)");

                RemoteHooking.WakeUpProcess();

                while (true) Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Interface.HandleError(e);
            }
        }
    }
}
