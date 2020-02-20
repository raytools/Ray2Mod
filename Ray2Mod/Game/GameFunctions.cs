using Ray2Mod.Game.Functions;

namespace Ray2Mod.Game
{
    public class GameFunctions
    {
        public GameFunctions(RemoteInterface remoteInterface)
        {
            Engine = new EngineFunctions(remoteInterface);
            Input = new InputFunctions(remoteInterface);
            Text = new TextFunctions(remoteInterface);
            Gfx = new GfxFunctions(remoteInterface);
            Gfx2 = new GfxSecondaryFunctions(remoteInterface);
        }

        public EngineFunctions Engine { get; }
        public InputFunctions Input { get; }
        public TextFunctions Text { get; }
        public GfxFunctions Gfx { get; }
        public GfxSecondaryFunctions Gfx2 { get; }
    }
}