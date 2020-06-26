using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IPO
    {
        public PhysicalObject* data;
        public Radiosity* radiosity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Radiosity
    {
        public int numItems;
        public RadiosityLOD* radiosityLODS;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RadiosityLOD
    {
        public int numberOfVertex;
        public VertexColor* vertexColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VertexColor
    {
        public short color_r;
        public short color_g;
        public short color_b;
        public short color_a;

        public float Red
        {
            get
            {
                return (color_r / (float)short.MaxValue);
            }
            set
            {
                color_r = (short)(value * short.MaxValue);
            }
        }

        public float Green
        {
            get
            {
                return (color_g / (float)short.MaxValue);
            }
            set
            {
                color_g = (short)(value * short.MaxValue);
            }
        }

        public float Blue
        {
            get
            {
                return (color_b / (float)short.MaxValue);
            }
            set
            {
                color_b = (short)(value * short.MaxValue);
            }
        }

        public float Alpha
        {
            get
            {
                return (color_a / (float)short.MaxValue);
            }
            set
            {
                color_a = (short)(value * short.MaxValue);
            }
        }
    }
}