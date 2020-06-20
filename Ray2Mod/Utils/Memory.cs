using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Utils
{
    public static unsafe class Memory
    {
        public static T* Malloc<T>(int size) where T : unmanaged => (T*)Marshal.AllocHGlobal(size);

        public static void Free<T>(T* ptr) where T : unmanaged => Marshal.FreeHGlobal((IntPtr)ptr);

        public static IntPtr GetPointerAtOffset(IntPtr address, params int[] offsets)
        {
            foreach (int offset in offsets)
            {
                address = Marshal.ReadIntPtr(address);
                address += offset;
            }

            return address;
        }

        public static void* GetPointerAtOffset(int address, params int[] offsets)
        {
            foreach (int offset in offsets)
            {
                address = *(int*)address;
                address += offset;
            }

            return (void*)address;
        }

        public static byte[] GetBytes(IntPtr address, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
                bytes[i] = Marshal.ReadByte(address + i);

            return bytes;
        }

        public static byte[] GetBytes(byte* address, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
                bytes[i] = *(address + i);

            return bytes;
        }

        public static T[] GetLPArray<T>(T* array, int length) where T : unmanaged
        {
            T[] result = new T[length];
            for (int i = 0; i < length; i++) {
                result[i] = array[i];
            }

            return result;
        }

        public static T* NewUnmanagedArray<T>(int size) where T : unmanaged
        {
            T* ptr = Malloc<T>(Marshal.SizeOf<T>() * size);
            return ptr;
        }

        public static T* ToUnmanagedArray<T>(this T[] array) where T : unmanaged
        {
            int length = array.Length;
            T* ptr = NewUnmanagedArray<T>(length);

            for (int i = 0; i < length; i++)
            {
                ptr[i] = array[i];
            }

            return ptr;
        }

    }
}