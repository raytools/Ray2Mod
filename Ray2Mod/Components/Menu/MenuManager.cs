using Ray2Mod.GameFunctions;
using Ray2Mod.Structs;

namespace Ray2Mod.Components.Menu
{
    public partial class Menu
    {
        public class MenuManager
        {
            public MenuManager(EngineFunctions engine, GfxFunctions gfx, InputFunctions input, TextFunctions text)
            {
                Engine = engine;
                Gfx = gfx;
                Input = input;
                Text = text;

                DefaultPosition = new Vector3(3, 13, 0);
            }

            private EngineFunctions Engine { get; }
            private GfxFunctions Gfx { get; }
            private InputFunctions Input { get; }
            private TextFunctions Text { get; }

            public Vector3 DefaultPosition { get; set; }

            public Menu NewMenu(params MenuItem[] items) =>
                new Menu(Engine, Gfx, Input, Text, DefaultPosition, 0, items);

            public Menu NewMenu(Vector3 position, params MenuItem[] items) =>
                new Menu(Engine, Gfx, Input, Text, position, 0, items);

            public Menu NewMenu(Vector3 position, float width, params MenuItem[] items) =>
                new Menu(Engine, Gfx, Input, Text, position, width, items);
        }
    }
}