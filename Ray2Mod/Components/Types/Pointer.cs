using System;
using System.Collections.Generic;

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

        public static Pointer<T>[] WrapPointerArray(T*[] ptrArray)
        {
            Pointer<T>[] results = new Pointer<T>[ptrArray.Length];
            for (int i = 0; i < ptrArray.Length; i++)
            {
                results[i] = (Pointer<T>)ptrArray[i];
            }

            return results;
        }

        public static T*[] PointerListToArray(List<Pointer<T>> ptrList)
        {
            T*[] results = new T*[ptrList.Count];
            for (int i = 0; i < ptrList.Count; i++)
            {
                results[i] = ptrList[i];
            }

            return results;
        }

        public override bool Equals(object obj)
        {
            if (obj is Pointer<T>)
            {
                return IntPtr == ((Pointer<T>)obj).IntPtr;
            }
            if (obj is IntPtr)
            {
                return IntPtr == (IntPtr)obj;
            }
            if (obj is int)
            {
                return IntPtr.ToInt32() == ((int)obj);
            }
            return false;
        }

        public override string ToString()
        {
            return $"Pointer<{typeof(T)}> @0x{(int)this:X}";
        }

        public override int GetHashCode() {
            return IntPtr.GetHashCode();
        }
    }
}