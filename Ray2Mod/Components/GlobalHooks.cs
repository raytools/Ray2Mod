using Ray2Mod.Game.Functions;

namespace Ray2Mod.Components
{
    public static class GlobalHooks
    {
        static GlobalHooks()
        {
        }

        private static HookManager GlobalHookManager;

        public static void InitGlobalHooks(RemoteInterface remoteInterface)
        {
            GlobalHookManager = new HookManager();

            GlobalActions.Interface = remoteInterface;

            GlobalHookManager.CreateHook(EngineFunctions.VEngine, GlobalActions.HVEngine);
            GlobalHookManager.CreateHook(TextFunctions.DrawsTexts, GlobalActions.HDrawsTexts);
            GlobalHookManager.CreateHook(InputFunctions.VirtualKeyToAscii, GlobalInput.HVirtualKeyToAscii);
        }
    }
}