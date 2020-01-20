using System;

namespace Ray2Mod
{
    public abstract class RemoteInterface : MarshalByRefObject
    {
        public abstract void Injected(int pid);

        public abstract void Log(string msgPacket);

        public abstract void HandleError(Exception e);

        public abstract void GameClosed();
    }
}