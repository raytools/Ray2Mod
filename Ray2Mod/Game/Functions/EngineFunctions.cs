using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;
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
            GEO_vEndModifyObject = new GameFunction<D_GEO_vEndModifyObject>(Offsets.EngineFunctions.GEO_vEndModifyObject);
            COL_fn_bCollideStaticSphereWithStaticIndexedTriangle = new GameFunction<D_COL_fn_bCollideStaticSphereWithStaticIndexedTriangle>(Offsets.EngineFunctions.COL_fn_bCollideStaticSphereWithStaticIndexedTriangle);
            COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2 = new GameFunction<D_COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2>(Offsets.EngineFunctions.COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2);
            AllocateMem = new GameFunction<D_AllocateMem>(Offsets.EngineFunctions.AllocateMem);
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

        #region GEO_vEndModifyObject

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int D_GEO_vEndModifyObject(GeometricObject* geometricObject);

        public static GameFunction<D_GEO_vEndModifyObject> GEO_vEndModifyObject { get; }

        #endregion




        #region COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate char D_COL_fn_bCollideStaticSphereWithStaticIndexedTriangle(int a);

        public static GameFunction<D_COL_fn_bCollideStaticSphereWithStaticIndexedTriangle> COL_fn_bCollideStaticSphereWithStaticIndexedTriangle { get; }

        #endregion

        #region COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int* D_COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2(int a1, int a2, int a3, int a4, int a5, int a6, short a7, char gvForCollision, char a9);

        public static GameFunction<D_COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2> COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2 { get; }

        #endregion


        #region AllocateMemory

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_AllocateMem(byte a1, int a2, int a3, uint dwBytes, int a5, char a6, char a7);

        public static GameFunction<D_AllocateMem> AllocateMem { get; }


        #endregion
    }
}