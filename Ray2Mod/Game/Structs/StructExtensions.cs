using Ray2Mod.Components.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs {
    public static class StructExtensions {
        
        public unsafe static Pointer<Perso>[] GetPersoArray(this Pointer<SuperObject>[] array)
        {
            return array.Select(p => 
            {
                if (p.StructPtr->type != SuperObjectType.Perso) {
                    return null;
                }

                return (Pointer<int>)p.StructPtr->engineObjectPtr;
            }).Where(p=>p!=null).ToArray().CastPointerArray<int, Perso>();
        }

        public unsafe static Pointer<S>[] CastPointerArray<T, S>(this Pointer<T>[] array) where T : unmanaged where S : unmanaged
        {
            return Array.ConvertAll(array, (p) =>
            {
                return new Pointer<S>(p.IntPtr);
            });
        }
    }
}
