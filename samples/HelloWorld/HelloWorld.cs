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

        private RemoteInterface Interface { get; set; }

        public void Run(RemoteInterface remoteInterface)
        {
            Manager = new HookManager();
            Game = new GameFunctions(remoteInterface);
            Interface = remoteInterface;

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

            Game.Engine.TextAfficheFunction.Hook = TestHook;
            Manager.CreateHook(Game.Engine.TextAfficheFunction);

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
                            Vector3 position = new Vector3(300, 300, 12);

                            int result = w.DrawText(raymanSPO, newSpo, alwTexteMenu, position, "/E1000:/O200:longer text?/E104:/D2000:/P2000:");

                            Interface.Log(result.ToString());
                        }
                    }
                });
            }
        }

        public unsafe int TestHook(int a1, int a2, int a3)
        {
            Interface.Log("== TextAfficheFunction begin ==");

            Interface.Log("Interpreter array:");
            int* nodes = (int*)a2;
            for (int i = 0; i < 8; i++)
            {
                Interface.Log($"0x{nodes[2*i]:X8}, 0x{nodes[2*i+1]:X8}");
            }

            int result = Game.Engine.TextAfficheFunction.Call(a1, a2, a3);

            Interface.Log($"result: {result}, a1: 0x{a1:X}, a2: 0x{a2:X}, a3: 0x{a3:X}");

            Interface.Log("== TextAfficheFunction end ==");

            return result;
        }
    }
}
