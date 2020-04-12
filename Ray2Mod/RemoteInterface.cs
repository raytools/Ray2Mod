using System;

namespace Ray2Mod
{
    public abstract class RemoteInterface : MarshalByRefObject
    {
        public abstract void Injected(int pid, string modName);

        public abstract void Log(string msgPacket, LogType type = LogType.Info);

        public abstract void HandleError(Exception e);

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

    public enum LogType : uint
    {
        Info,
        Error,
        Warning,
        Debug
    }
}