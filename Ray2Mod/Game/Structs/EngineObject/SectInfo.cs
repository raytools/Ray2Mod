using System.Runtime.InteropServices;

using Ray2Mod.Game.Structs.SPO;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SectInfo
    {
        public SuperObject* currentSector;
    }
}