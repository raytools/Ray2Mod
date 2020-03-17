using Ray2Mod.Components.Types;
using System;
using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Functions
{
    public static class GfxFunctions
    {
        static GfxFunctions()
        {
            VAddParticle = new GameFunction<DVAddParticle>(0x463390);
            VCreatePart = new GameFunction<DVCreatePart>(0x4600C0);
        }

        #region VAddParticle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DVAddParticle(uint particleType, IntPtr vector, IntPtr vector2, int texture, float a6);

        public static GameFunction<DVAddParticle> VAddParticle { get; }

        #endregion

        #region VCreatePart

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DVCreatePart(int particleBehavior, IntPtr position, int a3, float a4, float a5, float a6, int texture);

        public static GameFunction<DVCreatePart> VCreatePart { get; }

        #endregion

    }
}