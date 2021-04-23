using Ray2Mod.Components.Types;

namespace Ray2Mod.Game
{
    // Based on Rayman 2 GOG.com version

    public static class Offsets
    {
        public const int PauseScreen = 0x500FAA;
        public const int InputState = 0x50A560;
        public const int ObjectTypes = 0x005013E0;
        public const int GlobalGraphicsContext = 0x5004D4;
        public const int NumAlways = 0x004A6B18;
        public const int ActiveDynamicWorld = 0x0500FD0;
        public const int InactiveDynamicWorld = 0x500FC4;
        public const int FatherSector = 0x500FC0;
        public const int TexturePointer = 0x502680;
        public const int MemChannelsPointer = 0x501660;
        public const int EngineState = 0x500380;
        public const int MainChar = 0x500578;
        public const int Families = 0x500560;
        public const int InputStructure = 0x00509E60;
        public const int LocalizationStructure = 0x00500260;
        public const int gcGlobAleat = 0x4A7070; // Global Randomizer (byte)
        //500456 camera?

        public class EngineFunctions
        {
            public const int fn_vMakeCharacterThink = 0x4120D0;
            public const int VEngine = 0x40ADA0;
            public const int GetCurrentLevelName = 0x404DA0;
            public const int AskToChangeLevel = 0x4054D0;
            public const int Code4PersoLePlusProche = 0x476960;
            public const int MiscFunction = 0x47CC30;
            public const int GEO_vEndModifyObject = 0x41D1A0;
            public const int COL_fn_bCollideStaticSphereWithStaticIndexedTriangle = 0x00499440;
            public const int COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2 = 0x04990F0;
            public const int AllocateMem = 0x442420;
            public const int fn_vInitOneObject = 0x405E50;
            public const int fn_v3dDataCopyClone = 0x4186F0;
            public const int fn_vBrainCopyClone = 0x4180D0;
            public const int fn_p_stAllocateAlways = 0x40BCC0;
            public const int PLA_fn_hFindNextFreeSupObj = 0x40F140;
            public const int PLA_fn_vReleaseSuperObjectInHeap = 0x40F220;
            public const int PLA_fn_bSetNewState = 0x40FAA0;
            public const int fn_p_vGenAlloc = 0x443120;
            public const int fn_p_vDynAlloc = 0x442680;
            public const int fn_vGenFree = 0x443160;
            public const int fn_vDynFree = 0x442740;
            public const int fn_p_stReadAnalogJoystickMario = 0x46E450;
            public const int fn_p_stEvalTree = 0x480B90;
            public const int PLA_fn_vSetCurrFrame = 0x40FD30;
            public const int DoQueryPerformanceCounter = 0x45E930;
        }

        public class ParticleFunctions
        {
            public const int VAddParticle = 0x463390;
            public const int VCreatePart = 0x4600C0;
        }

        public class GfxSecondaryFunctions
        {
            public const int ClearZBufferRegion = 0x421FB0;
            public const int SwapSceneBuffer = 0x420F50;
            public const int WriteToViewportFinished = 0x420BB0;
        }

        public class InputFunctions
        {
            public const int VirtualKeyToAscii = 0x496110;
            public const int VReadInput = 0x496510;
        }

        public class TextFunctions
        {
            public const int DrawsTexts = 0x460670;
            public const int DrawText = 0x4660B0;
        }

        public class Globals
        {
            public const int g_hCurrentSuperObjPerso = 0x4B9C28;
        }
    }
}
