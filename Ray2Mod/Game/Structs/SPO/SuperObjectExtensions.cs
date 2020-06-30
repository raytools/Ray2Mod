using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs.EngineObject;
using System.Linq;

namespace Ray2Mod.Game.Structs.SPO
{
    public static class SuperObjectExtensions
    {
        public unsafe static Pointer<Sector>[] GetSectorArray(this SuperObject*[] array) => Pointer<SuperObject>.WrapPointerArray(array).GetSectorArray();

        public unsafe static Pointer<Perso>[] GetPersoArray(this SuperObject*[] array) => Pointer<SuperObject>.WrapPointerArray(array).GetPersoArray();

        public unsafe static Pointer<IPO>[] GetIPOArray(this SuperObject*[] array) => Pointer<SuperObject>.WrapPointerArray(array).GetIPOArray();

        public unsafe static Pointer<Sector>[] GetSectorArray(this Pointer<SuperObject>[] array)
        {
            return array.Select(p =>
            {
                if (p.StructPtr->type != SuperObjectType.Sector)
                {
                    return null;
                }

                return (Pointer<int>)p.StructPtr->engineObjectPtr;
            }).Where(p => p != null).ToArray().CastPointerArray<int, Sector>();
        }

        public unsafe static Pointer<Perso>[] GetPersoArray(this Pointer<SuperObject>[] array)
        {
            return array.Select(p =>
            {
                if (p.StructPtr->type != SuperObjectType.Perso)
                {
                    return null;
                }

                return (Pointer<int>)p.StructPtr->engineObjectPtr;
            }).Where(p => p != null).ToArray().CastPointerArray<int, Perso>();
        }

        public unsafe static Pointer<IPO>[] GetIPOArray(this Pointer<SuperObject>[] array)
        {
            return array.Select(p =>
            {
                if (p.StructPtr->type != SuperObjectType.IPO)
                {
                    return null;
                }

                return (Pointer<int>)p.StructPtr->engineObjectPtr;
            }).Where(p => p != null).ToArray().CastPointerArray<int, IPO>();
        }
    }
}