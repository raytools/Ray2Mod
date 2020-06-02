using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Utils
{
    public static class Memory
    {
        public static IntPtr GetPointerAtOffset(IntPtr address, params int[] offsets)
        {
            foreach (int offset in offsets)
            {
                address = Marshal.ReadIntPtr(address);
                address += offset;
            }

            return address;
        }

        public static unsafe void* GetPointerAtOffset(int address, params int[] offsets)
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

        public static unsafe byte[] GetBytes(byte* address, int length)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
                bytes[i] = *(address + i);

            return bytes;
        }

        public static unsafe T[] GetLPArray<T>(T* array, int length) where T : unmanaged
        {
            T[] result = new T[length];
            for (int i = 0; i < length; i++) {
                result[i] = array[i];
            }

            return result;
        }
    }
}