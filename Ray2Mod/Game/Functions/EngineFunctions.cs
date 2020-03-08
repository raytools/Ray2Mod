using System;
using System.Runtime.InteropServices;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;

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
            MiscFunction = new GameFunction<DMiscFunction>(0x47CC30);
            TextAfficheFunction = new GameFunction<DTextAfficheFunction>(0x475680);
        }

        public event Action Actions;

        #region VEngine

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte DVEngine();

        public GameFunction<DVEngine> VEngine { get; }

        private byte HVEngine()
        {
            byte engine = VEngine.Call();

            try
            {
                Actions?.Invoke();
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

        #region MiscFunction

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DMiscFunction(int superObject, int nodeInterpreter, int getSetParam);

        public GameFunction<DMiscFunction> MiscFunction { get; }

        #endregion

        #region TextAfficheFunction

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DTextAfficheFunction(int superObject, int nodeInterpreter, int getSetParam);

        public GameFunction<DTextAfficheFunction> TextAfficheFunction { get; }

        #endregion

    }
}