namespace Ray2Mod
{
    public interface IMod
    {
        /// <summary>
        /// Runs after the game is loaded and passes the remote interface to the mod.
        /// Most variables and pointers should be initialized at this point.
        /// </summary>
        /// <param name="remoteInterface"></param>
        void Run(RemoteInterface remoteInterface);
    }
}