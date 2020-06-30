using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DsgMem
    {
        public DsgVar** dsgVar;
        public byte* memoryBufferInitial;
        public byte* memoryBufferCurrent;
    }
}