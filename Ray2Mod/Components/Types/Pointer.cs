using System;

namespace Ray2Mod.Components.Types
{
    public unsafe class Pointer<T> where T : unmanaged
    {
        public Pointer(int pointer)
        {
            IntPtr = (IntPtr)pointer;
        }

        public Pointer(IntPtr intPtr)
        {
            IntPtr = intPtr;
        }

        public Pointer(T* structPtr)
        {
            IntPtr = (IntPtr)structPtr;
        }

        public IntPtr IntPtr { get; }
        public T* StructPtr => (T*)IntPtr;

        public static implicit operator T*(Pointer<T> ptr) => ptr.StructPtr;
        public static implicit operator IntPtr(Pointer<T> ptr) => ptr.IntPtr;
        public static implicit operator int(Pointer<T> ptr) => (int)ptr.IntPtr;

        public static implicit operator Pointer<T>(T* structPtr) => new Pointer<T>(structPtr);
        public static implicit operator Pointer<T>(IntPtr intPtr) => new Pointer<T>(intPtr);
    }
} 