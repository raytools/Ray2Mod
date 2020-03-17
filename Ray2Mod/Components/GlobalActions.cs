using Ray2Mod.Game.Functions;
using System;

namespace Ray2Mod.Components
{
    public static class GlobalActions
    {
        static GlobalActions()
        {
        }

        internal static RemoteInterface Interface { get; set; }

        public static event Action Engine;

        internal static byte HVEngine()
        {
            byte engine = EngineFunctions.VEngine.Call();

            try
            {
                Engine?.Invoke();
            }
            catch (Exception e)
            {
                Interface?.HandleError(e);
            }

            return engine;
        }

        public static event Action Text;

        internal static byte HDrawsTexts(int context)
        {
            try
            {
                Text?.Invoke();
            }
            catch (Exception e)
            {
                Interface?.HandleError(e);
            }

            return TextFunctions.DrawsTexts.Call(context);
        }
    }
}