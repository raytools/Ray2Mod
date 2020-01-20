using System;
using System.Runtime.InteropServices;
using EasyHook;

namespace Ray2Mod.Types
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

        private string Name { get; }
        public IntPtr Pointer { get; }
        public T Call { get; }
        private T Hook { get; set; }

        public bool AttachHook(EntryPoint detour)
        {
            if (Hook == null)
                return false;

            detour.Hooks[Name] = LocalHook.Create(Pointer, Hook, this);
            detour.Hooks[Name].ThreadACL.SetExclusiveACL(new[] {0});
            detour.Interface.Log($"Attached hook:\n{typeof(T).Name}");

            return true;
        }

        public bool DetachHook(EntryPoint detour)
        {
            if (!detour.Hooks.ContainsKey(Name))
                return false;

            detour.Hooks[Name].Dispose();
            detour.Hooks.Remove(Name);

            return true;
        }

        public void SetHook(T hook, EntryPoint detour)
        {
            DetachHook(detour);
            Hook = hook;
        }
    }
}