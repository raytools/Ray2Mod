using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Types
{
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