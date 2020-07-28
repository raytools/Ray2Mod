using System;
using System.Runtime.InteropServices;

using Ray2Mod.Components.Types;

namespace Ray2Mod.Game.Functions
{
    public static class GfxFunctions
    {
        static GfxFunctions()
        {
            VAddParticle = new GameFunction<DVAddParticle>(Offsets.ParticleFunctions.VAddParticle);
            VCreatePart = new GameFunction<DVCreatePart>(Offsets.ParticleFunctions.VCreatePart);
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