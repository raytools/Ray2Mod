using Ray2Mod.Game.Structs.LinkedLists;
using Ray2Mod.Game.Structs.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Families {
    
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Family {
        public int familyIndex;
        public LinkedList.HasHeaderPointers<State> states;
        public LinkedList.ListElement_HHP<ObjectList>* physicalListDefault;
        public LinkedList.HasHeaderPointers<ObjectList> objectLists;
        public int* boundingVolume;
        public int gap_0x18;
        public byte animBank;
        public byte gap_0x1D;
        public byte gap_0x1E;
        public byte gap_0x1F;
        public byte properties;
        public byte gap_0x21;
        public byte gap_0x22;
        public byte gap_0x23;
    }
}
