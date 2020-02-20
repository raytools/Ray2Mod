namespace Ray2Mod
{
    public interface IMod
    {
        /// <summary>
        /// Runs before the game is loaded and passes the remote interface to the mod.
        /// </summary>
        /// <param name="remoteInterface"></param>
        void Initialize(RemoteInterface remoteInterface);

        /// <summary>
        /// Runs after the game is loaded.
        /// All function hooks should be created here.
        /// </summary>
        void Run();
    }
}