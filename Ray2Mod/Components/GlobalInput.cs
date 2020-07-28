using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Ray2Mod.Game;
using Ray2Mod.Game.Functions;
using Ray2Mod.Game.Types;

namespace Ray2Mod.Components
{
    public static class GlobalInput
    {
        static GlobalInput()
        {
        }

        public static Dictionary<char, Action> Actions { get; } = new Dictionary<char, Action>();
        public static Dictionary<KeyCode, Action> KeycodeActions { get; } = new Dictionary<KeyCode, Action>();

        private static Action<char, KeyCode> _exclusiveInput;

        internal static short HVirtualKeyToAscii(byte ch, int a2)
        {
            short result = InputFunctions.VirtualKeyToAscii.Call(ch, a2);

            // Prevent custom binds from activating on pause screen
            if (Marshal.ReadByte((IntPtr)Offsets.PauseScreen) != 0)
            {
                return result;
            }

            if (_exclusiveInput == null)
            {
                lock (Actions)
                    lock (KeycodeActions)
                    {
                        if (Actions.TryGetValue((char)result, out Action action) ||
                            KeycodeActions.TryGetValue((KeyCode)ch, out action))
                        {
                            action.Invoke();
                        }
                    }
            }
            else
            {
                _exclusiveInput.Invoke((char)result, (KeyCode)ch);
            }

            return result;
        }

        public static void SetExclusiveInput(Action<char, KeyCode> handler)
        {
            _exclusiveInput = handler;
            DisableGameInput();
        }

        public static void ReleaseExclusiveInput()
        {
            _exclusiveInput = null;
            EnableGameInput();
        }

        private static IntPtr _inputState = (IntPtr)Offsets.InputState;
        private static int _previousInputState;

        public static void DisableGameInput()
        {
            if (_previousInputState == 0)
            {
                _previousInputState = Marshal.ReadInt32(_inputState);
            }

            Marshal.WriteInt32(_inputState, 0);
        }

        public static void EnableGameInput()
        {
            Marshal.WriteInt32(_inputState, _previousInputState);
            _previousInputState = 0;
        }
    }
}