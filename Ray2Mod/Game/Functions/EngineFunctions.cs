using System;
using System.Runtime.InteropServices;
using Ray2Mod.Components.Types;

namespace Ray2Mod.Game.Functions
{
    public class EngineFunctions : FunctionContainer
    {
        public EngineFunctions(RemoteInterface remoteInterface) : base(remoteInterface)
        {
            VEngine = new GameFunction<DVEngine>(0x40ADA0, HVEngine);
            GetCurrentLevelName = new GameFunction<DGetCurrentLevelName>(0x404DA0);
            AskToChangeLevel = new GameFunction<DAskToChangeLevel>(0x4054D0);
            Code4PersoLePlusProche = new GameFunction<DCode4PersoLePlusProche>(0x476960);
        }

        public event Action EngineLoop;

        #region VEngine

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte DVEngine();

        public GameFunction<DVEngine> VEngine { get; }

        private byte HVEngine()
        {
            byte engine = VEngine.Call();

            try
            {
                EngineLoop?.Invoke();
            }
            catch (Exception e)
            {
                Interface.HandleError(e);
            }

            return engine;
        }

        #endregion

        #region GetCurrentLevelName

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string DGetCurrentLevelName();

        public GameFunction<DGetCurrentLevelName> GetCurrentLevelName { get; }

        #endregion

        #region AskToChangeLevel

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DAskToChangeLevel(string levelName, byte save);

        public GameFunction<DAskToChangeLevel> AskToChangeLevel { get; }

        #endregion

        #region Code4PersoLePlusProche

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DCode4PersoLePlusProche(int a, int b, int c);

        public GameFunction<DCode4PersoLePlusProche> Code4PersoLePlusProche { get; }

        #endregion
    }
}