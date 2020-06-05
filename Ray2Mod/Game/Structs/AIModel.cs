using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AIModel
    {
        public BehaviorArray* behaviorsListNormal;
        public BehaviorArray* behaviorsListReflex;
        public DsgVar* dsgVar;
    }
}