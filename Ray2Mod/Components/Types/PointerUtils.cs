using System;
using System.Collections.Generic;

namespace Ray2Mod.Components.Types
{
    public static unsafe class PointerUtils
    {
        // Very dirty Linq-like extension method because pointers as generics aren't allowed...
        public static T*[] Where<T>(this T*[] array, Func<Pointer<T>, bool> filter) where T : unmanaged
        {
            List<Pointer<T>> result = new List<Pointer<T>>();
            foreach (var item in array)
            {
                if (filter.Invoke(new Pointer<T>(item)))
                {
                    result.Add(item);
                }
            }
            T*[] resultArray = new T*[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                resultArray[i] = result[i];
            }

            return resultArray;
        }
    }
}