using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Game.Structs.SPO;
using Ray2Mod.Utils;

namespace Ray2Mod.Components
{
    public static class GlobalHooks
    {
        static GlobalHooks()
        {
        }

        private static HookManager GlobalHookManager;

        #region Memory Allocation Fix

        // TODO: put this in a nice, separate class somewhere

        private static unsafe int* DynamicAllocation(uint size, char module) {
            return EngineFunctions.fn_p_vDynAlloc.Call(size);
        }

        private static unsafe void DynamicFreeing(uint size, char module) {
            return; // oh no, poor memory will never be freed
            // TODO: actually free stuff
        }

        private static unsafe SuperObject* FindNextFreeSupObj() {
            // Allocate data
            SuperObject* newSpo = new SuperObject().ToUnmanaged();
            newSpo->type = (SuperObjectType)1;
            newSpo->matrixPtr = Matrix.IdentityMatrix.ToUnmanaged();
            newSpo->matrixPtr2 = Matrix.IdentityMatrix.ToUnmanaged();
            newSpo->drawFlags = -1;
            newSpo->flags = new SuperObjectFlags(); // 0

            return newSpo;
        }
        #endregion

        private static unsafe void ReleaseSuperObj(SuperObject* spo) {
            return; // TODO: actually free stuff
        }

        public unsafe static void InitGlobalHooks(RemoteInterface remoteInterface)
        {
            GlobalHookManager = new HookManager();

            GlobalActions.Interface = remoteInterface;

            GlobalHookManager.CreateHook(EngineFunctions.VEngine, GlobalActions.HVEngine);
            GlobalHookManager.CreateHook(TextFunctions.DrawsTexts, GlobalActions.HDrawsTexts);
            GlobalHookManager.CreateHook(InputFunctions.VirtualKeyToAscii, GlobalInput.HVirtualKeyToAscii);

            GlobalHookManager.CreateHook(EngineFunctions.PLA_fn_hFindNextFreeSupObj, FindNextFreeSupObj);
            GlobalHookManager.CreateHook(EngineFunctions.PLA_fn_vReleaseSuperObjectInHeap, ReleaseSuperObj);
            GlobalHookManager.CreateHook(EngineFunctions.fn_p_vGenAlloc, DynamicAllocation);
            GlobalHookManager.CreateHook(EngineFunctions.fn_vGenFree, DynamicFreeing);
        }
    }
}