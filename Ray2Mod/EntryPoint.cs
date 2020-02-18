using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using EasyHook;

namespace Ray2Mod
{
    public abstract class EntryPoint : IEntryPoint
    {
        protected EntryPoint(RemoteHooking.IContext context, string channelName)
        {
            Interface = RemoteHooking.IpcConnectClient<RemoteInterface>(channelName);
            Interface.Injected(RemoteHooking.GetCurrentProcessId(), GetType().Assembly.GetName().Name);
        }

        private bool Running { get; set; } = true;

        public RemoteInterface Interface { get; }
        public Dictionary<string, LocalHook> Hooks { get; } = new Dictionary<string, LocalHook>();

        public void Run(RemoteHooking.IContext inContext, string inChannelName)
        {
            try
            {
                // Make sure the game is fully loaded before initializing hooks
                while (true)
                {
                    // Engine state: 1 - loading game, 5 - loading level, 9 - loaded
                    if (Marshal.ReadByte((IntPtr) 0x500380) > 8)
                        break;
                }

                InitializeMod();

                RemoteHooking.WakeUpProcess();
            }
            catch (Exception e)
            {
                Interface.HandleError(e);
            }

            while (Running) Thread.Sleep(500);
        }

        protected abstract void InitializeMod();

        protected void DisableMod()
        {
            foreach (KeyValuePair<string, LocalHook> hook in Hooks)
                hook.Value.Dispose();

            Running = false;
        }
    }
}
