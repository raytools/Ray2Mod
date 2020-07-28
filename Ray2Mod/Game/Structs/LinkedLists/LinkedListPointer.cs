using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.LinkedLists
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct LinkedListPointer<T> where T : unmanaged
    {
        public int ptr;

        public T* ToPointer()
        {
            return (T*)ptr;
        }
    }
}
