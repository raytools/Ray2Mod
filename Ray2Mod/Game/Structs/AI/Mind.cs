using Ray2Mod.Game.Structs.AI;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mind
    {
        public AIModel* mind;
        public Behavior* currentIntelligenceNormal;
        public Behavior* currentIntelligenceReflex;
        public DsgMem* dsgMem;

        public T* GetDsgVar<T>(int index, byte* buffer) where T : unmanaged => GetDsgVar<T>(index, buffer, out byte _);

        public T* GetDsgVar<T>(int index, byte* buffer, out byte arraySize) where T : unmanaged
        {
            if (index < 0 || index >= mind->dsgVar->dsgVarInfosLength)
            {
                throw new IndexOutOfRangeException("The DsgVar index is outside of the range of the DsgVarInfo-array");
            }
            DsgVarInfo info = mind->dsgVar->dsgVarInfos[index];

            if (DsgVarTypes.Map[info.type].IsArray)
            {
                arraySize = *(buffer + info.offsetInBuffer + 4);
                return (T*)(buffer + info.offsetInBuffer + 8);
            }
            else
            {
                arraySize = 1;
                return (T*)(buffer + info.offsetInBuffer);
            }
        }
    }
}