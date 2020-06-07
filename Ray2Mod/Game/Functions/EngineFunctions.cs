using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class EngineFunctions
    {
        static EngineFunctions()
        {
            VEngine = new GameFunction<DVEngine>(Offsets.EngineFunctions.VEngine);
            GetCurrentLevelName = new GameFunction<DGetCurrentLevelName>(Offsets.EngineFunctions.GetCurrentLevelName);
            AskToChangeLevel = new GameFunction<DAskToChangeLevel>(Offsets.EngineFunctions.AskToChangeLevel);
            Code4PersoLePlusProche = new GameFunction<DCode4PersoLePlusProche>(Offsets.EngineFunctions.Code4PersoLePlusProche);
            MiscFunction = new GameFunction<DMiscFunction>(Offsets.EngineFunctions.MiscFunction);
        }

        #region VEngine

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte DVEngine();

        public static GameFunction<DVEngine> VEngine { get; }

        #endregion

        #region GetCurrentLevelName

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string DGetCurrentLevelName();

        public static GameFunction<DGetCurrentLevelName> GetCurrentLevelName { get; }

        #endregion

        #region AskToChangeLevel

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DAskToChangeLevel(string levelName, byte save);

        public static GameFunction<DAskToChangeLevel> AskToChangeLevel { get; }

        #endregion

        #region Code4PersoLePlusProche

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DCode4PersoLePlusProche(int a, int b, int c);

        public static GameFunction<DCode4PersoLePlusProche> Code4PersoLePlusProche { get; }

        #endregion

        #region MiscFunction

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DMiscFunction(int superObject, int nodeInterpreter, int getSetParam);

        public static GameFunction<DMiscFunction> MiscFunction { get; }

        #endregion

    }
}