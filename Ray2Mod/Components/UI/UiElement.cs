using Ray2Mod.Game;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Types;
using System;

namespace Ray2Mod.Components.UI
{
    public abstract class UiElement
    {
        protected UiElement(GameFunctions game, Vector3 position)
        {
            Game = game;
            Position = position;
        }

        protected string Id { get; } = Guid.NewGuid().ToString();

        protected GameFunctions Game { get; }
        protected Vector3 Position { get; set; }
        protected UiElement Parent { get; set; }

        public virtual void Show(UiElement parent = null)
        {
            Parent = parent;

            CaptureInput();

            Game.Engine.Actions += DrawGraphics;
            Game.Text.Actions += DrawText;
        }

        public virtual void Hide()
        {
            Game.Engine.Actions -= DrawGraphics;
            Game.Text.Actions -= DrawText;

            ReleaseInput();
        }

        public virtual void GoBack()
        {
            Hide();
            Parent?.Show();
        }

        protected void CaptureInput()
        {
            Game.Input.DisableGameInput();
            Game.Input.ExclusiveInput = ProcessInput;
        }

        protected void ReleaseInput()
        {
            Game.Input.ExclusiveInput = null;
            Game.Input.EnableGameInput();
        }

        protected abstract void ProcessInput(char ch, KeyCode code);

        protected abstract void DrawGraphics();

        protected abstract void DrawText();

    }
}