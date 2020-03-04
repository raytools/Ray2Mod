using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Game;
using Ray2Mod.Game.Structs;
using Ray2Mod.Utils;

namespace Pointers
{
    public unsafe class Pointers : IMod
    {
        private HookManager Manager;
        private GameFunctions Game;

        public void Run(RemoteInterface remoteInterface)
        {
            Manager = new HookManager();
            Game = new GameFunctions(remoteInterface);
            Manager.InitMainLoops(Game);

            // Static addresses can be directly assigned to an unsafe pointer.
            byte* engineState = (byte*)0x500380;

            // Pointer/offset paths can be read at runtime using Memory.GetPointerAtOffset.
            Vector3* position = (Vector3*)Memory.GetPointerAtOffset(0x500560, 0x224, 0x310, 0x34, 0x0, 0x1ac);

            // Display values as in-game text
            Game.Text.Actions += () =>
            {
                // Utils.Text provides many extension methods that simplify in-game text formatting.
                // The second line is equivalent to:
                // "/O200:X/O0:\\" + (position->X).ToString("0.000", CultureInfo.InvariantCulture) + "/l:"
                string coordinates = "Coordinates=".NL() +
                                     "X".KeyValue(position->X.D3()).NL() +
                                     "Y".KeyValue(position->Y.D3()).NL() +
                                     "Z".KeyValue(position->Z.D3()).NL();

                Game.Text.CustomText(coordinates, 10, 5, 5);
                Game.Text.CustomText($"EngineState={*engineState}", 10, 5, 200);
            };
        }
    }
}
