using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class GfxSecondaryFunctions
    {
        static GfxSecondaryFunctions()
        {
            ClearZBufferRegion = new GameFunction<DClearZBufferRegion>(Offsets.GfxSecondaryFunctions.ClearZBufferRegion);
            SwapSceneBuffer = new GameFunction<DSwapSceneBuffer>(Offsets.GfxSecondaryFunctions.SwapSceneBuffer);
            WriteToViewportFinished = new GameFunction<DWriteToViewportFinished>(Offsets.GfxSecondaryFunctions.WriteToViewportFinished);
        }

        #region ClearZBufferRegion

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DClearZBufferRegion(int unk1, int unk2, int unk3, int unk4);

        public static GameFunction<DClearZBufferRegion> ClearZBufferRegion { get; }

        #endregion

        #region SwapSceneBuffer

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DSwapSceneBuffer();

        public static GameFunction<DSwapSceneBuffer> SwapSceneBuffer { get; }

        #endregion

        #region WriteToViewportFinished

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DWriteToViewportFinished(int _, short word5004D0);

        public static GameFunction<DWriteToViewportFinished> WriteToViewportFinished;

        #endregion
    }
}