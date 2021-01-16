using System;
using System.Threading.Tasks;

using Ray2Mod.Game;
using Ray2Mod.Game.Functions;

namespace Ray2Mod.Components
{
    public static class GlobalActions
    {
        static GlobalActions()
        {
            Task.Run(PollEngineState);
        }

        internal static RemoteInterface Interface { get; set; }

        public delegate void StateEventDelegate(EnumEngineState previous, EnumEngineState current);
        public static event StateEventDelegate EngineStateChanged;

        private static byte previousEngineState;

        private static Task PollEngineState()
        {
            while (true)
            {
                unsafe
                {
                    // invoke EngineStateChanged if the state byte changes
                    byte engineState = *(byte*)Offsets.EngineState;
                    if (engineState != previousEngineState)
                    {
                        Interface?.Log($";;;;Engine state changed from {previousEngineState} to {engineState}.", LogType.Debug);
                        EngineStateChanged?.Invoke((EnumEngineState)previousEngineState, (EnumEngineState)engineState);
                        previousEngineState = engineState;
                    }
                }
                //await Task.Delay(20);
            }
        }

        public static event Action PreEngine;
        public static event Action Engine;

        internal static byte HVEngine()
        {
            try
            {
                // invoke engine actions
                PreEngine?.Invoke();
            }
            catch (Exception e)
            {
                Interface?.HandleError(e);
            }

            byte engine = EngineFunctions.VEngine.Call();

            try
            {
                // invoke engine actions
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
