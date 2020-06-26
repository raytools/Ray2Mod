using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StandardGame
    {
        public int familyID;
        public int modelID;
        public int instanceID;
        public SuperObject* superObjectPtr;
        public int gap1;
        public int gap2;
        public int gap3;
        public int gap4;
        public byte hitpoints;
        public byte gap5;
        public byte hitpointsMax;
        public byte gap6;
        public int customBits;
        public byte isAPlatform;
        public byte updateCheckByte;
        public byte transparencyZoneMin;
        public byte transparencyZoneMax;
        public int customBitsInitial;

        public void SetCustomBit(int bitNum)
        {
            customBits |= 1 << bitNum;
        }

        public void UnsetCustomBit(int bitNum)
        {
            customBits &= ~(1 << bitNum);
        }

        public void ResetCustomBits()
        {
            customBits = customBitsInitial;
        }

    }


}