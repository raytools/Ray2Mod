using Ray2Mod.GameFunctions;
using Ray2Mod.Structs;
using Ray2Mod.Types;
using Ray2Mod.Utils;
using System;
using System.Collections.Generic;

namespace Ray2Mod.Components.Menu
{
    public partial class Menu
    {
        private Menu(EngineFunctions engine, GfxFunctions gfx, InputFunctions input, TextFunctions text,
            Vector3 position, float width = 0, params MenuItem[] items)
        {
            Engine = engine;
            Gfx = gfx;
            Input = input;
            Text = text;
            Items = new List<MenuItem>(items);

            Position = position;
            Width = width > 0 ? width : CalculateWidth();
        }

        private EngineFunctions Engine { get; }
        private GfxFunctions Gfx { get; }
        private InputFunctions Input { get; }
        private TextFunctions Text { get; }

        private string Id { get; } = Guid.NewGuid().ToString();

        private Vector3 Position { get; }
        private float Width { get; }
        private Menu ParentMenu { get; set; }

        public List<MenuItem> Items { get; }

        private int _selected;
        public int Selected
        {
            get => _selected;
            set
            {
                if (value >= 0 && value < Items.Count)
                    _selected = value;
            }
        }

        public void Show(Menu parentMenu = null)
        {
            ParentMenu = parentMenu;
            Selected = 0;

            Input.DisableGameInput();
            Input.ExclusiveInput = ProcessInput;

            Engine.Actions.Set(Id, DrawGraphics);
            Text.Actions.Set(Id, DrawText);
        }

        public void Hide()
        {
            Engine.Actions.Delete(Id);
            Text.Actions.Delete(Id);

            Input.ExclusiveInput = null;
            Input.EnableGameInput();
        }

        private void ProcessInput(char ch, KeyCode code)
        {
            if (code == KeyCode.Enter)
            {
                Hide();
                if (Items[Selected].IsSubmenu) Items[Selected].Submenu.Show(this);
                else Items[Selected].Action.Invoke();
            }
            else if (code == KeyCode.Backspace)
            {
                Hide();
                ParentMenu?.Show();
            }
            else if (code == KeyCode.Up)
            {
                Selected--;
            }
            else if (code == KeyCode.Down)
            {
                Selected++;
            }
        }

        private void DrawGraphics()
        {
            Vector3 vpos2 = new Vector3(Position.X + Width + 2,Position.Y + 2 + Items.Count * 4, 0);
            using (StructPtr pos1 = new StructPtr(Position), pos2 = new StructPtr(vpos2))
            {
                Gfx.VAddParticle.Call(110, pos1, pos2, TexturePointers.blueSparkTexture, 190);
            }
        }

        private void DrawText()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Text.CustomText(i == Selected ? Items[i].Name.Yellow() : Items[i].Name, 9, (Position.X + 1) * 10, (Position.Y + 2 + i * 4) * 10);
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