using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class EngineFunctions
    {
        static EngineFunctions()
        {
            VEngine = new GameFunction<DVEngine>(0x40ADA0);
            GetCurrentLevelName = new GameFunction<DGetCurrentLevelName>(0x404DA0);
            AskToChangeLevel = new GameFunction<DAskToChangeLevel>(0x4054D0);
            Code4PersoLePlusProche = new GameFunction<DCode4PersoLePlusProche>(0x476960);
            MiscFunction = new GameFunction<DMiscFunction>(0x47CC30);
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