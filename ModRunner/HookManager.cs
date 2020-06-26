using EasyHook;
using Ray2Mod;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;

namespace ModRunner
{
    public class HookManager
    {
        public HookManager(string libraryName, RemoteInterface remote, params string[] processNames)
        {
            Library = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), libraryName);

            ProcessNames = processNames;
            Remote = remote;
        }

        private string _channelName;
        private IpcServerChannel ipc;
        private RemoteInterface Remote { get; }
        private string[] ProcessNames { get; }
        private string Library { get; }

        public bool IsHookAttached { get; set; }

        public void Inject(string[] dllsToLoad)
        {
            _channelName = null;

            if (ipc == null)
            {
                ipc = RemoteHooking.IpcCreateServer<RemoteInterface>(ref _channelName,
                    WellKnownObjectMode.Singleton, Remote);
            }

            Thread injectionThread = new Thread(() =>
            {
                Remote.Log("Attempting to inject...");
                while (!IsHookAttached)
                {
                    int processId = GetProcessId();

                    if (processId == 0)
                    {
                        Remote.Log("Cannot find process, retrying in 5s...", LogType.Warning);
                        Thread.Sleep(5000);
                        continue;
                    }

                    try
                    {
                        RemoteHooking.Inject(processId, InjectionOptions.DoNotRequireStrongName,
                            Library, Library, _channelName, dllsToLoad);

                        IsHookAttached = true;
                        Remote.Log("Injection finished.");
                    }
                    catch (Exception e)
                    {
                        Remote.Log("Injection error:", LogType.Error);
                        Remote.Log(e.ToString(), LogType.Error);
                        Remote.Log("Retrying in 5s...");

                        IsHookAttached = false;
                        Thread.Sleep(5000);
                    }
                }
            });
            injectionThread.IsBackground = true;
            injectionThread.Start();
        }

        private int GetProcessId()
        {
            foreach (string name in ProcessNames)
            {
                Process[] processes = Process.GetProcessesByName(name);

                if (processes.Length > 0)
                    return processes[0].Id;
            }

            return 0;
        }
    }
}