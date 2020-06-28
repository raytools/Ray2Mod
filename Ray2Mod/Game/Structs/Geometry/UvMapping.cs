using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Geometry
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UvMappingList
    {
        /// <summary>
        /// Rayman 2 only has one item in the array, Rayman 3 can have more.
        /// </summary>
        public UvMapping* mapping;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UvMapping
    {
        /// <summary>
        /// Array with indexes per vertex pointing to an item in the UV array (size of the array is the amount of triangles * 3)
        /// </summary>
        public int* items;
    }
}