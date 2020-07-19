using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Structs.EngineObject;
using Ray2Mod.Game.Structs.Geometry;
using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Game.Structs.States;
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
            PLA_fn_hFindNextFreeSupObj = new GameFunction<D_PLA_fn_hFindNextFreeSupObj>(Offsets.EngineFunctions.PLA_fn_hFindNextFreeSupObj);
            PLA_fn_vReleaseSuperObjectInHeap = new GameFunction<D_PLA_fn_vReleaseSuperObjectInHeap>(Offsets.EngineFunctions.PLA_fn_vReleaseSuperObjectInHeap);
            fn_p_vGenAlloc = new GameFunction<D_fn_p_vGenAlloc>(Offsets.EngineFunctions.fn_p_vGenAlloc);
            fn_p_vDynAlloc = new GameFunction<D_fn_p_vDynAlloc>(Offsets.EngineFunctions.fn_p_vDynAlloc);
            fn_vGenFree = new GameFunction<D_fn_vGenFree>(Offsets.EngineFunctions.fn_vGenFree);
            fn_vDynFree = new GameFunction<D_fn_vDynFree>(Offsets.EngineFunctions.fn_vDynFree);
            PLA_fn_bSetNewState = new GameFunction<D_PLA_fn_bSetNewState>(Offsets.EngineFunctions.PLA_fn_bSetNewState);
            fn_p_stReadAnalogJoystickMario = new GameFunction<D_fn_p_stReadAnalogJoystickMario>(Offsets.EngineFunctions.fn_p_stReadAnalogJoystickMario);
            fn_p_stEvalTree = new GameFunction<D_fn_p_stEvalTree>(Offsets.EngineFunctions.fn_p_stEvalTree);
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
        public unsafe delegate void D_fn_p_stAllocateAlways(int modelID, Perso* dynamicWorld, Perso* callingSuperObject, int a4, Matrix* matrix);

        public static GameFunction<D_fn_p_stAllocateAlways> fn_p_stAllocateAlways { get; }

        #endregion fn_p_stAllocateAlways




        #region PLA_fn_hFindNextFreeSupObj

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate SuperObject * D_PLA_fn_hFindNextFreeSupObj();

        public static GameFunction<D_PLA_fn_hFindNextFreeSupObj> PLA_fn_hFindNextFreeSupObj { get; }

        #endregion PLA_fn_hFindNextFreeSupObj

        #region PLA_fn_vReleaseSuperObjectInHeap

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_PLA_fn_vReleaseSuperObjectInHeap(SuperObject * spo);

        public static GameFunction<D_PLA_fn_vReleaseSuperObjectInHeap> PLA_fn_vReleaseSuperObjectInHeap { get; }

        #endregion PLA_fn_vReleaseSuperObjectInHeap

        #region fn_p_vGenAlloc
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int* D_fn_p_vGenAlloc(uint size, char module);

        public static GameFunction<D_fn_p_vGenAlloc> fn_p_vGenAlloc { get; }

        #endregion fn_p_vGenAlloc

        #region fn_p_vDynAlloc
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int* D_fn_p_vDynAlloc(uint size);

        public static GameFunction<D_fn_p_vDynAlloc> fn_p_vDynAlloc { get; }

        #endregion fn_p_vDynAlloc


        #region fn_vGenFree
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_vGenFree(uint size, char module);

        public static GameFunction<D_fn_vGenFree> fn_vGenFree { get; }

        #endregion fn_vGenFree

        #region fn_vDynFree
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void D_fn_vDynFree(uint size);

        public static GameFunction<D_fn_vDynFree> fn_vDynFree { get; }

        #endregion fn_vDynFree

        #region PLA_fn_bSetNewState
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate char D_PLA_fn_bSetNewState(SuperObject * persoSpo, LinkedList.ListElement_HHP<State>* state, char force, char withEvents, char setAnim);

        public static GameFunction<D_PLA_fn_bSetNewState> PLA_fn_bSetNewState { get; }

        #endregion PLA_fn_bSetNewState

        #region fn_p_stReadAnalogJoystickMario
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int D_fn_p_stReadAnalogJoystickMario(SuperObject* persoSpo, int* nodeInterp);

        public static GameFunction<D_fn_p_stReadAnalogJoystickMario> fn_p_stReadAnalogJoystickMario { get; }

        #endregion fn_p_stReadAnalogJoystickMario

        #region fn_p_stEvalTree
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int D_fn_p_stEvalTree(SuperObject* persoSpo, int* nodeInterp, int* param);

        public static GameFunction<D_fn_p_stEvalTree> fn_p_stEvalTree { get; }

        #endregion fn_p_stEvalTree
    }
}