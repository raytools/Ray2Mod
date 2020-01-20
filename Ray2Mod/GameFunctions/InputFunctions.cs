using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ray2Mod.Types;

namespace Ray2Mod.GameFunctions
{
    public class InputFunctions : FunctionContainer
    {
        public InputFunctions(EntryPoint entry) : base(entry)
        {
            VirtualKeyToAscii = new GameFunction<DVirtualKeyToAscii>(0x496110, HVirtualKeyToAscii);
            VReadInput = new GameFunction<DVReadInput>(0x496510, HVReadInput);
        }

        public Dictionary<char, Action> Actions { get; } = new Dictionary<char, Action>();
        public Dictionary<KeyCode, Action> KeycodeActions { get; } = new Dictionary<KeyCode, Action>();

        public Action<char, KeyCode> ExclusiveInput { get; set; }

        #region VirtualKeyToAscii

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate short DVirtualKeyToAscii(byte ch, int a2);

        public GameFunction<DVirtualKeyToAscii> VirtualKeyToAscii { get; }

        private short HVirtualKeyToAscii(byte ch, int a2)
        {
            short result = VirtualKeyToAscii.Call(ch, a2);

            Interface.Log($"VirtualKeyToAscii result: {(char)result}, char: {ch}, a2: {a2}");

            // Prevent custom binds from activating on pause screen
            if (Marshal.ReadByte((IntPtr) 0x500faa) != 0) return result;

            if (ExclusiveInput == null)
            {
                lock (Actions)
                {
                    lock (KeycodeActions)
                    {
                        if (Actions.TryGetValue((char)result, out Action action) ||
                            KeycodeActions.TryGetValue((KeyCode)ch, out action))
                            action.Invoke();
                    }
                }
            }
            else ExclusiveInput.Invoke((char)result, (KeyCode)ch);

            return result;
        }

        #endregion

        #region VReadInput

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate short DVReadInput(int a1);

        public GameFunction<DVReadInput> VReadInput { get; }

        private short HVReadInput(int a1)
        {
            short result = VReadInput.Call(a1);

            Interface.Log($"VReadInput Output: {result}, Pointer:");
            Interface.Log($"0x{Convert.ToString(a1, 16)}");

            return result;
        }

        #endregion

        private int dword50A560;

        public void DisableGameInput()
        {
            if (dword50A560 == 0)
                dword50A560 = Marshal.ReadInt32((IntPtr) 0x50A560);

            Marshal.WriteInt32((IntPtr) 0x50A560, 0);
        }

        public void EnableGameInput()
        {
            Marshal.WriteInt32((IntPtr) 0x50A560, dword50A560);
            dword50A560 = 0;
        }
    }
}