using Ray2Mod.Components.Types;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class TextFunctions
    {
        static TextFunctions()
        {
            DrawsTexts = new GameFunction<FDrawsTexts>(0x460670);
            DrawText = new GameFunction<FDrawText>(0x4660B0);
        }

        #region DrawsTexts

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte FDrawsTexts(int context);

        public static GameFunction<FDrawsTexts> DrawsTexts { get; }

        #endregion

        #region DrawText

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FDrawText(int a1, IntPtr textStruct);

        public static GameFunction<FDrawText> DrawText { get; }

        #endregion
    }
}