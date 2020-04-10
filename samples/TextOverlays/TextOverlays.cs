using Ray2Mod;
using Ray2Mod.Components.Text;

namespace TextOverlays
{
    public class TextOverlays : IMod
    {
        private RemoteInterface Interface { get; set; }

        public void Run(RemoteInterface remoteInterface)
        {
            Interface = remoteInterface;

            // Regular, static text.
            // The X and Y values are coordinates on the screen space, mapped to a 1000x1000 area.
            TextOverlay plain = new TextOverlay("Regular text", 10, 5 ,5).Show();

            // Dynamically updating text.
            // The delegate TextOverlay.UpdateText is evaluated on every frame (if assigned).
            // It can be assigned instead of text in the constructor, or later at any point.
            int frames = 0;
            TextOverlay counter = new TextOverlay((previousText) =>
            {
                string newText = $"Frame counter={frames}";
                frames++;
                return newText;
            }, 10, 5 ,35).Show();

            // Previous text is passed to the UpdateText method as a parameter.
            // This example appends 1 dot to the text every 10 frames, up to a total of 20.
            // It also uses the frame counter from the previous example.
            int dots = 0;
            TextOverlay append = new TextOverlay("Some dots", 10, 5, 65).Show();
            append.UpdateText = (text) =>
            {
                if (frames % 10 == 0 && dots < 20)
                {
                    text += '.';
                    dots++;
                }
                return text;
            };

            // The delegate TextOverlay.UpdateProperties can be used for advanced text manipulation.
            // It is evaluated on every frame, after UpdateText.
            // The TextOverlay object itself is passed as a parameter.
            // This lets you reuse functions and delegates for multiple text objects.
            plain.UpdateProperties = MoveTextOffscreen;
        }

        // This example will move the text object to the right
        // and hide it once it's no longer visible on screen.
        private void MoveTextOffscreen(TextOverlay overlay)
        {
            overlay.X += 3;
            Interface.Log($"Text overlay visible at X:{overlay.X}");
            if (overlay.X > 1000)
            {
                // Note: While the text overlay is hidden, both UpdateText and UpdateProperties
                // are _not_ evaluated - notice the "Text overlay visible" messages
                // are no longer sent after calling Hide().
                overlay.Hide();
                Interface.Log($"Text overlay hidden");
            }
        }
    }
}
