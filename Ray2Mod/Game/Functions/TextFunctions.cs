using System;
using System.Runtime.InteropServices;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;

namespace Ray2Mod.Game.Functions
{
    public class TextFunctions : FunctionContainer
    {
        public TextFunctions(RemoteInterface remoteInterface) : base(remoteInterface)
        {
            DrawsTexts = new GameFunction<FDrawsTexts>(0x460670, HDrawsTexts);
            DrawText = new GameFunction<FDrawText>(0x4660B0);
        }

        public event Action TextLoop;

        #region DrawsTexts

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte FDrawsTexts(int context);

        public GameFunction<FDrawsTexts> DrawsTexts { get; }

        private byte HDrawsTexts(int context)
        {
            try
            {
                TextLoop?.Invoke();
            }
            catch (Exception e)
            {
                Interface.HandleError(e);
            }

            return DrawsTexts.Call(context);
        }

        #endregion

        #region DrawText

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FDrawText(int a1, IntPtr textStruct);

        public GameFunction<FDrawText> DrawText { get; }

        #endregion

        public void CustomText(string text, float size, float x, float y, byte alpha = 255)
        {
            Text2D textStruct = CreateTextStruct(text, size, x, y, alpha);
            DrawTextStruct(textStruct);
        }

        public Text2D CreateTextStruct(string text, float size, float x, float y, byte alpha = 255)
        {
            return new Text2D
            {
                text = text,
                size = size,
                positionX = x,
                positionY = y,
                alphaByte = alpha
            };
        }

        public void DrawTextStruct(Text2D textObject)
        {
            using (StructPtr textPtr = new StructPtr(textObject))
            {
                DrawText.Call(0x5004D4, textPtr);
            }
        }
    }
}