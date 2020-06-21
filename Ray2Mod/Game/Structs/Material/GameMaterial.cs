using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
