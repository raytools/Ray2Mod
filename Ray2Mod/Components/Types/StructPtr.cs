using Ray2Mod.Utils;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Components.Types
{
    public unsafe class StructPtr<T> : IDisposable where T : unmanaged
    {
        public StructPtr(T obj)
        {
            Pointer = Memory.Malloc<T>(Marshal.SizeOf(obj));
            Marshal.StructureToPtr(obj, (IntPtr)Pointer, false);
        }

        public T* Pointer { get; private set; }

        public static implicit operator T*(StructPtr<T> ptr) => ptr.Pointer;

        private void ReleaseUnmanagedResources()
        {
            if (Pointer != null)
            {
                Memory.Free(Pointer);
                Pointer = null;
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~StructPtr()
        {
            ReleaseUnmanagedResources();
        }
    }

    [Obsolete("This class is obsolete - use StructPtr<T> instead.")]
    public class StructPtr : IDisposable
    {
        public StructPtr(object obj)
        {
            if (obj != null)
            {
                Pointer = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
                Marshal.StructureToPtr(obj, Pointer, false);
            }
            else Pointer = IntPtr.Zero;
        }

        public IntPtr Pointer { get; private set; }

        public static implicit operator IntPtr(StructPtr ptr) => ptr.Pointer;


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

        ~StructPtr() => ReleaseUnmanagedResources();
    }
}