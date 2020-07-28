﻿using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.AI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mind
    {
        public AIModel* aiModel;
        public Intelligence* intelligenceNormal;
        public Intelligence* intelligenceReflex;
        public DsgMem* dsgMem;

        public T* GetDsgVar<T>(int index, byte* buffer) where T : unmanaged
        {
            return GetDsgVar<T>(index, buffer, out byte _);
        }

        public T* GetDsgVar<T>(int index, byte* buffer, out byte arraySize) where T : unmanaged
        {
            if (index < 0 || index >= aiModel->dsgVar->dsgVarInfosLength)
            {
                throw new IndexOutOfRangeException("The DsgVar index is outside of the range of the DsgVarInfo-array");
            }
            DsgVarInfo info = aiModel->dsgVar->dsgVarInfos[index];

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