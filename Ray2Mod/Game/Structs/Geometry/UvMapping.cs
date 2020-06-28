using Ray2Mod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Geometry
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UvMapping
    {
        public short mapping_0;
        public short mapping_1;
        public short mapping_2;
    }
}