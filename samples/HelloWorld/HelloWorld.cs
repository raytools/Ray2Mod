using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Utils;

namespace HelloWorld
{
    public class HelloWorld : IMod
    {
        public void Run(RemoteInterface remoteInterface)
        {
            // By subscribing to the GlobalActions.Text event,
            // the following code will be executed inside the text drawing loop.
            // Attempting to draw text outside of that loop will crash the game.
            GlobalActions.Text += () =>
            {
                // Draw 2D text overlay on screen.
                // The X and Y values are coordinates on the screen space, mapped to a 1000x1000 area.
                TextUtils.TextOverlay("Hello World", 10, 5, 5);
                TextUtils.TextOverlay("This text is red and transparent".Red(), 10, 5, 30, 180);
            };
        }
    }
}
