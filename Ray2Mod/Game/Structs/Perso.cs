using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Perso
    {
        public int p3dData;
        public StandardGame* stdGamePtr;
        public int dynam;
        public Brain* brain;

        public List<DsgVarInfoAndValues> GetDsgVarList()
        {
            var result = new List<DsgVarInfoAndValues>();
            if (brain == null || brain->mind == null || brain->mind->dsgMem == null || brain->mind->dsgMem->dsgVar == null) {
                return result;
            }

            DsgMem* dsgMem = brain->mind->dsgMem;
            DsgVar* dsgVar = *(dsgMem->dsgVar);

            for(int i=0;i<dsgVar->dsgVarInfosLength;i++) {
                var info = dsgVar->dsgVarInfos[i];
                result.Add(new DsgVarInfoAndValues()
                {
                    info = info,
                    valuePtrCurrent = new IntPtr(dsgMem->memoryBufferCurrent + info.offsetInBuffer),
                    valuePtrInitial = new IntPtr(dsgMem->memoryBufferInitial + info.offsetInBuffer)
                });
            }

            return result;
        }
    } 
}