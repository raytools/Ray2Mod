using Ray2Mod.Components.Types;
using Ray2Mod.Game.Functions;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Components.Text
{
    public class TextOverlay
    {
        public TextOverlay(string text, float size, float x, float y, byte alpha = 255) : this(size, x, y, alpha)
        {
            Text = text;
        }

        public TextOverlay(Func<string, string> updateText, float size, float x, float y, byte alpha = 255) : this(size, x, y, alpha)
        {
            UpdateText = updateText;
        }

        private TextOverlay(float size, float x, float y, byte alpha)
        {
            Size = size;
            X = x;
            Y = y;
            Alpha = alpha;
        }

        private TextData Data { get; } = new TextData();

        public string Text
        {
            get => Data.text;
            set => Data.text = value;
        }
        public float Size
        {
            get => Data.size;
            set => Data.size = value;
        }
        public float X
        {
            get => Data.positionX;
            set => Data.positionX = value;
        }
        public float Y
        {
            get => Data.positionY;
            set => Data.positionY = value;
        }
        public byte Alpha
        {
            get => Data.alphaByte;
            set => Data.alphaByte = value;
        }

        public Func<string, string> UpdateText { get; set; }
        public Action<TextOverlay> UpdateProperties { get; set; }

        public bool Visible { get; private set; }

        public TextOverlay Show()
        {
            if (!Visible)
            {
                GlobalActions.Text += DrawText;
                Visible = true;
            }

            return this;
        }

        public TextOverlay Hide()
        {
            if (Visible)
            {
                GlobalActions.Text -= DrawText;
                Visible = false;
            }
            return this;
        }

        private void DrawText()
        {
            if (UpdateText != null) Text = UpdateText(Text);
            UpdateProperties?.Invoke(this);

            using (StructPtr ptr = new StructPtr(Data))
            {
                TextFunctions.DrawText.Call(0x5004D4, ptr);
            }
        }
    }

    //TODO: identical to Text2D struct, but mutable. not sure what to do about this
    [StructLayout(LayoutKind.Sequential)]
    internal class TextData
    {
        internal string text;
        internal float positionX;
        internal float positionY;
        internal float size;
        internal byte alphaByte;
        internal byte gap11;
        internal byte highlight;
        internal byte options;
        internal int dword14;
        internal byte flag3;
    }
}