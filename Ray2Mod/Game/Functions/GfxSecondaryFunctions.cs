using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class GfxSecondaryFunctions
    {
        static GfxSecondaryFunctions()
        {
            ClearZBufferRegion = new GameFunction<DClearZBufferRegion>(0x421FB0);
            SwapSceneBuffer = new GameFunction<DSwapSceneBuffer>(0x420F50);
            WriteToViewportFinished = new GameFunction<DWriteToViewportFinished>(0x420BB0);
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