using EasyHook;
using Ray2Mod.Components.Types;
using System;
using System.Collections.Generic;

namespace Ray2Mod.Components
{
    public class HookManager
    {
        public Dictionary<string, LocalHook> Hooks { get; } = new Dictionary<string, LocalHook>();

        public bool CreateHook<T>(GameFunction<T> function) where T : Delegate
        {
            if (function.Hook == null)
                return false;

            Hooks[function.Name] = LocalHook.Create(function.Pointer, function.Hook, function);
            Hooks[function.Name].ThreadACL.SetExclusiveACL(new[] { 0 });

            return true;
        }

        public bool RemoveHook<T>(GameFunction<T> function) where T : Delegate
        {
            if (!Hooks.ContainsKey(function.Name))
                return false;

            Hooks[function.Name].Dispose();
            Hooks.Remove(function.Name);

            return true;
        }
    }
}