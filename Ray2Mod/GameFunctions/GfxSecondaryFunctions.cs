using System.Runtime.InteropServices;
using Ray2Mod.Types;

namespace Ray2Mod.GameFunctions
{
    public class GfxSecondaryFunctions : FunctionContainer
    {
        public GfxSecondaryFunctions(EntryPoint entry) : base(entry)
        {
            ClearZBufferRegion = new GameFunction<DClearZBufferRegion>(0x421FB0);
            SwapSceneBuffer = new GameFunction<DSwapSceneBuffer>(0x420F50);
            WriteToViewportFinished = new GameFunction<DWriteToViewportFinished>(0x420BB0);
        }

        #region ClearZBufferRegion

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DClearZBufferRegion(int unk1, int unk2, int unk3, int unk4);

        public GameFunction<DClearZBufferRegion> ClearZBufferRegion { get; }

        #endregion

        #region SwapSceneBuffer

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DSwapSceneBuffer();

        public GameFunction<DSwapSceneBuffer> SwapSceneBuffer { get; }

        #endregion

        #region WriteToViewportFinished

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DWriteToViewportFinished(int _, short word5004D0);

        public GameFunction<DWriteToViewportFinished> WriteToViewportFinished;

        #endregion
    }
}