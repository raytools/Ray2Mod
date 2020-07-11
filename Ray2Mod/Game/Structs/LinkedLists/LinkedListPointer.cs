using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.LinkedLists {

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct LinkedListPointer<T> where T:unmanaged{
        public int ptr;

        public T* ToPointer() {
            return (T*)ptr;
        }
    }
}
