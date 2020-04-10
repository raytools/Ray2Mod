using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Components.Text;
using Ray2Mod.Utils;

namespace HelloWorld
{
    public class HelloWorld : IMod
    {
        public void Run(RemoteInterface remoteInterface)
        {
            // Draw 2D text overlay on screen.
            // The X and Y values are coordinates on the screen space, mapped to a 1000x1000 area.
            TextOverlay hello = new TextOverlay("Hello World", 10, 5, 5).Show();
            TextOverlay colors = new TextOverlay("This text is red and transparent".Red(), 10, 5, 30, 180).Show();

            // Use RemoteInterface.Log() to display text in the mod loader console.
            remoteInterface.Log("Hello World");
        }
    }
}
