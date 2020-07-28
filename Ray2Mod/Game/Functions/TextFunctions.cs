using System;
using System.Runtime.InteropServices;

using Ray2Mod.Components.Types;

namespace Ray2Mod.Game.Functions
{
    public static class TextFunctions
    {
        static TextFunctions()
        {
            DrawsTexts = new GameFunction<FDrawsTexts>(Offsets.TextFunctions.DrawsTexts);
            DrawText = new GameFunction<FDrawText>(Offsets.TextFunctions.DrawText);
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