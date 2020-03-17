using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class InputFunctions
    {
        static InputFunctions()
        {
            VirtualKeyToAscii = new GameFunction<DVirtualKeyToAscii>(0x496110);
            VReadInput = new GameFunction<DVReadInput>(0x496510);
        }

        #region VirtualKeyToAscii

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate short DVirtualKeyToAscii(byte ch, int a2);

        public static GameFunction<DVirtualKeyToAscii> VirtualKeyToAscii { get; }

        #endregion

        #region VReadInput

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate short DVReadInput(int a1);

        public static GameFunction<DVReadInput> VReadInput { get; }

        #endregion
    }
}