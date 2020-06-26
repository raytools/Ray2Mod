using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Material
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VisualMaterial
    {
        public uint flags;
        public Vector4 ambientCoef;
        public Vector4 diffuseCoef;
        public Vector4 specularCoef;
        public Vector4 color;
        public uint field_0x48;
        public uint off_texture;
        public float currentScrollX;
        public float currentScrollY;
        public float scrollX;
        public float scrollY;
        public uint scrollMode;
        public uint refreshNumber;
        public uint animTexturesFirst;
        public uint animTexturesCurrent;
        public ushort numAnimTextures;
        public ushort field_0x70;
        public uint field_0x74;
    }
}