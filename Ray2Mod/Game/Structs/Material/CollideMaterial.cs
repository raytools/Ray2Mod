using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Material
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CollideMaterial
    {
        public ushort type;
        public CollisionFlags identifier;
        public uint typeForAI;
    }
}