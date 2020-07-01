using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.EngineObject
{
    // Sizeof = 0x118
    public unsafe struct Perso3dData
    {
        public int* stateInitial;
        public int* stateCurrent;
        public int* state2;

        public int* objectList;
        public int* objectListInitial;
        public int* family;

        // Exactly 0x100 bytes after this
        public fixed char fixedBuffer[0x100];
    }
}