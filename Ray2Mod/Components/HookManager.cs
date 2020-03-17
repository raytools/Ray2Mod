using EasyHook;
using Ray2Mod.Components.Types;
using System;
using System.Collections.Generic;
using Ray2Mod.Game;

namespace Ray2Mod.Components
{
    public class HookManager
    {
        private Dictionary<Delegate, LocalHook> Hooks { get; } = new Dictionary<Delegate, LocalHook>();

        public bool CreateHook<T>(GameFunction<T> function, T hook) where T : Delegate
        {
            if (hook == null)
                return false;

            Hooks[hook] = LocalHook.Create(function.Pointer, hook, function);
            Hooks[hook].ThreadACL.SetExclusiveACL(new[] { 0 });

            return true;
        }

        public bool RemoveHook<T>(GameFunction<T> function, T hook) where T : Delegate
        {
            if (!Hooks.ContainsKey(hook))
                return false;

            Hooks[hook].Dispose();
            Hooks.Remove(hook);

            return true;
        }
    }
}