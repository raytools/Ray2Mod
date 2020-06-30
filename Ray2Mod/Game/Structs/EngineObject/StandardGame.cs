using Ray2Mod.Game.Structs.SPO;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.EngineObject
{
    // Size = 0x34
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StandardGame
    {
        public int familyID;
        public int modelID;
        public int instanceID;
        public SuperObject* superObjectPtr;
        public int field_0x10;
        public int field_0x14_someInitializeSetting;
        public int field_0x18;
        public int field_0x1C;
        public byte hitpoints;
        public byte field_21;
        public byte hitpointsMax;
        public byte field_23;
        public int customBits;
        public byte isAPlatform;
        public byte updateCheckByte;
        public byte transparencyZoneMin;
        public byte transparencyZoneMax;
        public int customBitsInitial;
        public int field_2E;
        public short field_32;

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