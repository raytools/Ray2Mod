using Ray2Mod.Components.Types;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class InputFunctions
    {
        static InputFunctions()
        {
            VirtualKeyToAscii = new GameFunction<DVirtualKeyToAscii>(Offsets.InputFunctions.VirtualKeyToAscii);
            VReadInput = new GameFunction<DVReadInput>(Offsets.InputFunctions.VReadInput);
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