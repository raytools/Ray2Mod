using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.States {
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StateTransition {
        public State * targetState;
        public State * stateToGo;
        public byte linkingType;
        public byte padding0;
        public byte padding1;
        public byte padding2;
    }
}
