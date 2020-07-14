using Ray2Mod.Game.Structs.LinkedLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.States {

    // State should always be used as a parameter of a LinkedList.ListElement_HHP
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct State {
        public LinkedList.HasHeaderPointers<StateTransition> stateTransitions;
        public LinkedList.HasHeaderPointers<LinkedListPointer<int>> prohibitStates; // can't use State here...
    }
}
