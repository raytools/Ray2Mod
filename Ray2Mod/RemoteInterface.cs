using System;

namespace Ray2Mod
{
    public abstract class RemoteInterface : MarshalByRefObject
    {
        public abstract void Injected(int pid, string modName);

        public abstract void Log(string msgPacket, uint id = 0);

        public abstract void HandleError(Exception e);
    }
}