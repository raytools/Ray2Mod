using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Components.Types
{
    public class GameFunction<T> where T : Delegate
    {
        public GameFunction(int pointer)
        {
            Guid = System.Guid.NewGuid().ToString();
            Pointer = (IntPtr) pointer;
            Call = Marshal.GetDelegateForFunctionPointer<T>(Pointer);
        }

        public string Guid { get; }
        public IntPtr Pointer { get; }
        public T Call { get; }
    }
}