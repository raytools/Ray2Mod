using System;

using Ray2Mod.Components.Types;

namespace Ray2Mod.Game.Structs
{
    public static class StructExtensions
    {

        public unsafe static Pointer<S>[] CastPointerArray<T, S>(this Pointer<T>[] array) where T : unmanaged where S : unmanaged
        {
            return Array.ConvertAll(array, (p) =>
            {
                return new Pointer<S>(p.IntPtr);
            });
        }
    }
}
