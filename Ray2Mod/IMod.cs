namespace Ray2Mod
{
    public interface IMod
    {
        /// <summary>
        /// Runs before the game is loaded and passes the remote interface to the mod.
        /// Note: While most function hooks can be created at this point,
        /// variables are probably not initialized yet.
        /// </summary>
        /// <param name="remoteInterface"></param>
        void Initialize(RemoteInterface remoteInterface);

        /// <summary>
        /// Runs after the game is loaded.
        /// Most variables and pointers should be initialized at this point.
        /// </summary>
        void Run();
    }
}