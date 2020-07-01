using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Structs.EngineObject;
using Ray2Mod.Game.Structs.Geometry;
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
            fn_vInitOneObject = new GameFunction<D_fn_vInitOneObject>(Offsets.EngineFunctions.fn_vInitOneObject);
            fn_v3dDataCopyClone = new GameFunction<D_fn_v3dDataCopyClone>(Offsets.EngineFunctions.fn_v3dDataCopyClone);
            fn_vBrainCopyClone = new GameFunction<D_fn_vBrainCopyClone>(Offsets.EngineFunctions.fn_vBrainCopyClone);
            fn_p_stAllocateAlways = new GameFunction<D_fn_p_stAllocateAlways>(Offsets.EngineFunctions.fn_p_stAllocateAlways);
        }

        #region VEngine

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte DVEngine();

        public static GameFunction<DVEngine> VEngine { get; }

        #endregion VEngine

        #region GetCurrentLevelName

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string DGetCurrentLevelName();

        public static GameFunction<DGetCurrentLevelName> GetCurrentLevelName { get; }

        #endregion GetCurrentLevelName

        #region AskToChangeLevel

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DAskToChangeLevel(string levelName, byte save);

        public static GameFunction<DAskToChangeLevel> AskToChangeLevel { get; }

        #endregion AskToChangeLevel

        #region Code4PersoLePlusProche

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DCode4PersoLePlusProche(int a, int b, int c);

        public static GameFunction<DCode4PersoLePlusProche> Code4PersoLePlusProche { get; }

        #endregion Code4PersoLePlusProche

        #region MiscFunction

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DMiscFunction(int superObject, int nodeInterpreter, int getSetParam);

        public static GameFunction<DMiscFunction> MiscFunction { get; }

        #endregion MiscFunction

        #region GEO_vEndModifyObject

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int D_GEO_vEndModifyObject(GeometricObject* geometricObject);

        public static GameFunction<D_GEO_vEndModifyObject> GEO_vEndModifyObject { get; }

        #endregion GEO_vEndModifyObject

        #region COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate char D_COL_fn_bCollideStaticSphereWithStaticIndexedTriangle(int a);

        public static GameFunction<D_COL_fn_bCollideStaticSphereWithStaticIndexedTriangle> COL_fn_bCollideStaticSphereWithStaticIndexedTriangle { get; }

        #endregion COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        #region COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int* D_COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2(int a1, int a2, int a3, int a4, int a5, int a6, short a7, char gvForCollision, char a9);

        public static GameFunction<D_COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2> COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2 { get; }

        #endregion COL_fn_bCollideStaticSphereWithStaticIndexedTriangle

        #region AllocateMemory

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_AllocateMem(byte a1, int a2, int a3, uint dwBytes, int a5, char a6, char a7);

        public static GameFunction<D_AllocateMem> AllocateMem { get; }

        #endregion AllocateMemory

        #region fn_vInitOneObject

        /// <summary>
        /// Initialize one EngineObject of type Perso
        /// </summary>
        /// <param name="perso">The Perso</param>
        /// <param name="initializeMode">0 for normal initialization, 1 for always objects</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_vInitOneObject(Perso* perso, int initializeMode);

        public static GameFunction<D_fn_vInitOneObject> fn_vInitOneObject { get; }

        #endregion fn_vInitOneObject

        #region fn_v3dDataCopyClone

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_v3dDataCopyClone(Perso* target, Perso* source);

        public static GameFunction<D_fn_v3dDataCopyClone> fn_v3dDataCopyClone { get; }

        #endregion fn_v3dDataCopyClone

        #region fn_vBrainCopyClone

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_vBrainCopyClone(Perso* target, Perso* source);

        public static GameFunction<D_fn_vBrainCopyClone> fn_vBrainCopyClone { get; }

        #endregion fn_vBrainCopyClone

        #region fn_p_stAllocateAlways

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_p_stAllocateAlways(Perso* target, Perso* source);

        public static GameFunction<D_fn_p_stAllocateAlways> fn_p_stAllocateAlways { get; }

        #endregion fn_p_stAllocateAlways
    }
}