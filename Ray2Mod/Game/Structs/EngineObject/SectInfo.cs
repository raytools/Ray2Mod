using Ray2Mod.Game.Structs.SPO;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.EngineObject
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SectInfo
    {
        public SuperObject* currentSector;
    }
}