using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Input {
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct InputStructure {
        [FieldOffset(0x700)]
        public int numEntryActions;
        [FieldOffset(0x704)]
        public EntryElement* entryElementArray;

        public EntryElement[] EntryElements
        {
            get
            {
                EntryElement[] result = new EntryElement[numEntryActions];
                for (int i = 0; i < numEntryActions; i++) {
                    result[i] = entryElementArray[i];
                }
                return result;
            }
        }
    }
}
