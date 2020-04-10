using Ray2Mod.Components.Types;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Types;
using Ray2Mod.Utils;
using System.Collections.Generic;

namespace Ray2Mod.Components.UI.Menu
{
    public class Menu : UiElement
    {
        public Menu(params MenuItem[] items) : this(new Vector3(3, 13, 0), items) { }

        public Menu(Vector3 position, params MenuItem[] items) : this(position, 0, items) { }

        public Menu(Vector3 position, float width = 0, params MenuItem[] items) : base(position)
        {
            Items = new List<MenuItem>(items);
            Width = width > 0 ? width : CalculateWidth();
        }

        private float Width { get; }
        private List<MenuItem> Items { get; }

        private int _selected;
        private int Selected
        {
            get => _selected;
            set
            {
                if (value >= 0 && value < Items.Count)
                    _selected = value;
            }
        }

        public override void Show(UiElement parent = null)
        {
            Selected = 0;
            base.Show(parent);
        }

        protected override void ProcessInput(char ch, KeyCode code)
        {
            if (code == KeyCode.Enter)
            {
                Hide();
                Items[Selected].Action?.Invoke();
                Items[Selected].Submenu?.Show(this);
            }
            else if (code == KeyCode.Backspace)
                GoBack();
            else if (code == KeyCode.Up)
                Selected--;
            else if (code == KeyCode.Down)
                Selected++;
        }

        protected override void DrawGraphics()
        {
            Vector3 vpos2 = new Vector3(Position.X + Width + 2, Position.Y + 2 + Items.Count * 4, 0);

            using (StructPtr pos1 = new StructPtr(Position), pos2 = new StructPtr(vpos2))
                GfxFunctions.VAddParticle.Call(110, pos1, pos2, TexturePointers.blueSparkTexture, 190);
        }

        protected override void DrawText()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                string name = i == Selected ? Items[i].Name.Yellow() : Items[i].Name;
                float posX = (Position.X + 1) * 10;
                float posY = (Position.Y + 2 + i * 4) * 10;

                TextUtils.TextOverlay(name, 9, posX, posY);
            }
        }

        private int CalculateWidth()
        {
            int newWidth = 0;
            foreach (MenuItem item in Items)
            {
                int itemWidth = item.Name.Length * 2;
                if (itemWidth > newWidth)
                    newWidth = itemWidth;
            }

            return newWidth;
        }
    }
}