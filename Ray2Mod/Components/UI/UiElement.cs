using Ray2Mod.Game;
using Ray2Mod.Game.Structs;
using Ray2Mod.Game.Types;
using System;

namespace Ray2Mod.Components.UI
{
    public abstract class UiElement
    {
        protected UiElement(Vector3 position)
        {
            Position = position;
        }

        protected string Id { get; } = Guid.NewGuid().ToString();

        protected Vector3 Position { get; set; }
        protected UiElement Parent { get; set; }

        public virtual void Show(UiElement parent = null)
        {
            Parent = parent;

            CaptureInput();

            GlobalActions.Engine += DrawGraphics;
            GlobalActions.Text += DrawText;
        }

        public virtual void Hide()
        {
            GlobalActions.Engine -= DrawGraphics;
            GlobalActions.Text -= DrawText;

            ReleaseInput();
        }

        public virtual void GoBack()
        {
            Hide();
            Parent?.Show();
        }

        protected void CaptureInput()
        {
            GlobalInput.SetExclusiveInput(ProcessInput);
        }

        protected void ReleaseInput()
        {
            GlobalInput.ReleaseExclusiveInput();
        }

        protected abstract void ProcessInput(char ch, KeyCode code);

        protected abstract void DrawGraphics();

        protected abstract void DrawText();

    }
}