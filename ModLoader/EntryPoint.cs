using EasyHook;
using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Game;
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
                Interface.Log($"Using {OtherUtils.ApiProductName} v{OtherUtils.ApiVersion}");

                GlobalHooks.InitGlobalHooks(Interface);
                GlobalActions.EngineStateChanged += OnGameExit;

                Loader = new Loader(Interface);
                Loader.LoadMods(dllsToLoad);

                // Make sure the game is fully loaded
                while (Marshal.ReadByte((IntPtr)Offsets.EngineState) < 9) { }

                Loader.InitMods();

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

        private void OnGameExit(byte previous, byte current)
        {
            if (current == 2)
            {
                try
                {
                    Interface?.ProcessExit();
                }
                catch (Exception) { }
                
            }
        }
    }
}
