using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Components.Types
{
    public class GameFunction<T> where T : Delegate
    {
        public GameFunction(int pointer)
        {
            Name = Guid.NewGuid().ToString();
            Pointer = (IntPtr) pointer;
            Call = Marshal.GetDelegateForFunctionPointer<T>(Pointer);
        }

        public GameFunction(int pointer, T hook) : this(pointer)
        {
            Hook = hook;
        }

        public string Name { get; }
        public IntPtr Pointer { get; }
        public T Call { get; }
        public T Hook { get; set; }

    }
}