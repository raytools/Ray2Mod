using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Material
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameMaterial
    {
        public VisualMaterial* visualMaterial;
        public int mechanicsMaterial;
        public int soundMaterial;
        public CollideMaterial* collideMaterial;
    }
}