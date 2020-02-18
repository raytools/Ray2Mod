using Ray2Mod.Game;
using Ray2Mod.Utils;
using System;
using System.Collections.Generic;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Types;

namespace Ray2Mod.Components.Menu
{
    public class Menu
    {
        public Menu(GameFunctions gameFunctions, params MenuItem[] items) : this(gameFunctions, new Vector3(3, 13, 0), 0, items) { }

        public Menu(GameFunctions gameFunctions, Vector3 position, params MenuItem[] items) : this(gameFunctions, position, 0, items) { }

        private Menu(GameFunctions gameFunctions, Vector3 position, float width = 0, params MenuItem[] items)
        {
            Game = gameFunctions;
            Position = position;
            Width = width > 0 ? width : CalculateWidth();
            Items = new List<MenuItem>(items);
        }

        private string Id { get; } = Guid.NewGuid().ToString();
        private GameFunctions Game { get; }

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

            Game.Input.DisableGameInput();
            Game.Input.ExclusiveInput = ProcessInput;

            Game.Engine.EngineLoop += DrawGraphics;
            Game.Text.TextLoop += DrawText;
        }

        public void Hide()
        {
            Game.Engine.EngineLoop -= DrawGraphics;
            Game.Text.TextLoop -= DrawText;

            Game.Input.ExclusiveInput = null;
            Game.Input.EnableGameInput();
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
            Vector3 vpos2 = new Vector3(Position.X + Width + 2, Position.Y + 2 + Items.Count * 4, 0);
            using (StructPtr pos1 = new StructPtr(Position), pos2 = new StructPtr(vpos2))
            {
                Game.Gfx.VAddParticle.Call(110, pos1, pos2, TexturePointers.blueSparkTexture, 190);
            }
        }

        private void DrawText()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Game.Text.CustomText(i == Selected ? Items[i].Name.Yellow() : Items[i].Name, 9, (Position.X + 1) * 10, (Position.Y + 2 + i * 4) * 10);
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