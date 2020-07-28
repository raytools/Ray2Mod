using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.LinkedLists;

namespace Ray2Mod.Game.Structs.States
{

    // State should always be used as a parameter of a LinkedList.ListElement_HHP
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct State
    {
        public LinkedList.HasHeaderPointers<StateTransition> stateTransitions;
        public LinkedList.HasHeaderPointers<LinkedListPointer<int>> prohibitStates; // can't use State here...
    }
}
