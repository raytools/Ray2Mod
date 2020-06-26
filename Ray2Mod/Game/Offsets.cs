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

        public class EngineFunctions
        {
            public const int VEngine = 0x40ADA0;
            public const int GetCurrentLevelName = 0x404DA0;
            public const int AskToChangeLevel = 0x4054D0;
            public const int Code4PersoLePlusProche = 0x476960;
            public const int MiscFunction = 0x47CC30;
            public const int GEO_vEndModifyObject = 0x41D1A0;
            public const int COL_fn_bCollideStaticSphereWithStaticIndexedTriangle = 0x00499440;
            public const int COL_fn_vCollideStaticGeomObj1WithStaticGeomObj2 = 0x04990F0;
            public const int AllocateMem = 0x442420;
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
    }
}
