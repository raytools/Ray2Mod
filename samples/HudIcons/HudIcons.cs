using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Structs.MathStructs;
using Ray2Mod.Utils;

namespace HudIcons
{
    public class HudIcons : IMod
    {
        public void Run(RemoteInterface remoteInterface)
        {
            // All particles, HUD icons and other GFX objects should always be drawn in the main engine loop.
            GlobalActions.Engine += () =>
            {
                // Create 2 vectors - upper-left and lower-right corner of the icon.
                // For HUD icons, the screen space is mapped to a 100x100 area.
                // Only X and Y coordinates are used to position the icon.
                // The Z coordinate of the 2nd vector is the alpha/transparency value.
                Vector3 vPos1 = new Vector3(5, 5, 0);
                Vector3 vPos2 = new Vector3(20, 20, 255);

                // StructPtr copies the vector structure to unmanaged memory and provides a pointer.
                // The memory is freed automatically at the end of the using block.
                using (StructPtr pos1 = new StructPtr(vPos1), pos2 = new StructPtr(vPos2))
                {
                    // Particle type 125 is the HUD icon type.
                    // The last parameter "a6" varies between particle types - in this case it's an index.
                    // A specific index value can only be used by one icon. If multiple icons are drawn
                    // with the same index, only the last one will be displayed.
                    GfxFunctions.VAddParticle.Call(125, pos1, pos2, TexturePointers.rayIcon, 11);
                }
            };
        }
    }
}