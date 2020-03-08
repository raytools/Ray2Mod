using System.Collections.Generic;
using Ray2Mod;
using Ray2Mod.Components;
using Ray2Mod.Components.Types;
using Ray2Mod.Game;
using Ray2Mod.Game.Structs;
using Ray2Mod.Utils;

namespace HelloWorld
{
    public class HelloWorld : IMod
    {
        // Instances of HookManager and GameFunctions have to be stored
        // as fields or properties to prevent the hooks from unloading.
        private HookManager Manager { get; set; }
        private GameFunctions Game { get; set; }

        public void Run(RemoteInterface remoteInterface)
        {
            Manager = new HookManager();
            Game = new GameFunctions(remoteInterface);

            // InitMainLoops creates hooks to 3 functions: VEngine (main engine loop),
            // DrawsTexts (text drawing loop), and VirtualKeyToAscii (input reading function).
            // The first two are executed on every frame.
            // The input function is executed whenever a keyboard input occurs.
            Manager.InitMainLoops(Game);

            // By subscribing to the TextFunctions.Actions event,
            // the following code will be executed inside the text drawing loop.
            // Attempting to draw text outside of that loop will crash the game.
            Game.Text.Actions += () =>
            {
                // Draw 2D text overlay on screen.
                // The X and Y values are coordinates on the screen space, mapped to a 1000x1000 area.
                Game.Text.CustomText("Hello World", 10, 5, 5);
                Game.Text.CustomText("This text is red and transparent".Red(), 10, 5, 30, 180);
            };

            unsafe
            {
                Game.Input.Actions.Add(('h'), () =>
                {
                    World w = new World(remoteInterface);
                    w.ReadObjectNames();

                    Dictionary<string, Pointer<SuperObject>> activeSuperObjects = w.GetActiveSuperObjects();
                    Dictionary<string, Pointer<Perso>> always = w.GetAlwaysObjects();
                    foreach (var a in always)
                    {
                        remoteInterface.Log(a.Key + ", " + (int)a.Value);
                    }
                    foreach (var a in activeSuperObjects)
                    {
                        remoteInterface.Log(a.Key + ", " + (int)a.Value);
                    }

                    // Alw_Projectile_Rayman
                    // ALW_TexteMenu
                    // ALW_FaisceauGrappin

                    if (always.ContainsKey("ALW_TexteMenu"))
                    {
                        var alwTexteMenu = always["ALW_TexteMenu"].StructPtr;
                        if (activeSuperObjects.ContainsKey("Rayman"))
                        {
                            var raymanSPO = activeSuperObjects["Rayman"];
                            //w.GenerateAlwaysObject(raymanSPO, projectilePerso, raymanSPO.StructPtr->matrixPtr2->position);
                            SuperObject* newSpo = null;
                            w.DrawText(raymanSPO, newSpo, alwTexteMenu, raymanSPO.StructPtr->matrixPtr2->position, "test");
                        }
                    }
                });
            }
        }
    }
}
