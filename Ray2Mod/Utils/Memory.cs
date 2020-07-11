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
            for (int i = 0; i < length; i++)
            {
                result[i] = array[i];
            }

            return result;
        }

        public static T* ToUnmanaged<T>(this T @struct) where T : unmanaged
        {
            T* ptr = Malloc<T>(Marshal.SizeOf(@struct));
            // This causes a memory leak if not freed after using. Too bad!
            *ptr = @struct;

            return ptr;
        }

        public static T* ToUnmanaged<T>(this T[] array) where T : unmanaged
        {
            int length = array.Length;
            T* ptr = NewUnmanagedArray<T>(length);

            for (int i = 0; i < length; i++)
            {
                ptr[i] = array[i];
            }

            return ptr;
        }

        public static T** ToUnmanaged<T>(this T*[] ptrArray) where T : unmanaged
        {
            int length = ptrArray.Length;
            T** ptr = (T**)NewUnmanagedArray<int>(length);

            for (int i = 0; i < length; i++)
            {
                ptr[i] = ptrArray[i];
            }

            return ptr;
        }

        public static T* NewUnmanagedArray<T>(int size) where T : unmanaged
        {
            T* ptr = Malloc<T>(Marshal.SizeOf<T>() * size);
            return ptr;
        }

        public static bool IsNull<T>(T* ptr) where T : unmanaged => ptr == null || ptr == (T*)0xFFFFFFFF;
    }
}