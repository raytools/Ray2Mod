using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Families {
    public unsafe struct ObjectList {
        public int* off_objListStart;
        public int* off_objList_2;
        public short numEntries;
        public short gap;
    }
}
