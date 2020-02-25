using System;
using System.Runtime.InteropServices;
using Ray2Mod.Components.Types;
using Ray2Mod.Game.Structs;

namespace Ray2Mod.Game.Functions
{
    public class GfxFunctions : FunctionContainer
    {
        public GfxFunctions(RemoteInterface remoteInterface) : base(remoteInterface)
        {
            VAddParticle = new GameFunction<DVAddParticle>(0x463390);
            VCreatePart = new GameFunction<DVCreatePart>(0x4600C0);
        }

        #region VAddParticle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DVAddParticle(uint particleType, IntPtr vector, IntPtr vector2, int texture, float a6);

        public GameFunction<DVAddParticle> VAddParticle { get; }

        #endregion

        #region VCreatePart

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DVCreatePart(int particleBehavior, IntPtr position, int a3, float a4, float a5, float a6, int texture);

        public GameFunction<DVCreatePart> VCreatePart { get; }

        #endregion

    }
}