using Ray2Mod.Game.Functions;

namespace Ray2Mod.Game
{
    public class GameFunctions
    {
        public GameFunctions(EntryPoint entryPoint)
        {
            Engine = new EngineFunctions(entryPoint);
            Input = new InputFunctions(entryPoint);
            Text = new TextFunctions(entryPoint);
            Gfx = new GfxFunctions(entryPoint);
            Gfx2 = new GfxSecondaryFunctions(entryPoint);
        }

        public EngineFunctions Engine { get; }
        public InputFunctions Input { get; }
        public TextFunctions Text { get; }
        public GfxFunctions Gfx { get; }
        public GfxSecondaryFunctions Gfx2 { get; }
    }
}