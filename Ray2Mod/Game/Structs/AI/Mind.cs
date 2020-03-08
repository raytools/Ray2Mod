using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.AI {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mind {
        public AIModel* aiModel;
        public Intelligence* intelligenceNormal;
        public Intelligence* intelligenceReflex;
        public DsgMem* dsgMem;
    }
}
