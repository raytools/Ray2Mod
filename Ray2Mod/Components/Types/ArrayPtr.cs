using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Components.Types
{
    /// <summary>
    /// Pointer to an Int32 array in unmanaged memory.
    /// </summary>
    public class ArrayPtr : IDisposable
    {
        /// <summary>
        /// Allocates unmanaged memory and writes the specified array.
        /// </summary>
        /// <param name="array">The array to write to unmanaged memory.</param>
        public ArrayPtr(int[] array)
        {
            if (array != null)
            {
                Pointer = Marshal.AllocHGlobal(array.Length * 4);

                for (int i = 0; i < array.Length; i++)
                    Marshal.WriteInt32(Pointer, i * 4, array[i]);
            }
            else Pointer = IntPtr.Zero;
        }

        /// <summary>
        /// Allocates unmanaged memory for an empty array of specified size.
        /// </summary>
        /// <param name="elements">The size of the new array.</param>
        public ArrayPtr(int elements)
        {
            Pointer = elements > 0
                ? Marshal.AllocHGlobal(elements * 4)
                : IntPtr.Zero;
        }

        public IntPtr Pointer { get; private set; }

        public static implicit operator IntPtr(ArrayPtr ptr) => ptr.Pointer;

        private void ReleaseUnmanagedResources()
        {
            if (Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~ArrayPtr() => ReleaseUnmanagedResources();
    }
}