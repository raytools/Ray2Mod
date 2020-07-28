using System;

namespace Ray2Mod.Game.Structs.Input
{
    public static class Functions
    {

        public enum FunctionType
        {
            Unknown,
            And,
            Or,
            Not,
            KeyJustPressed,
            KeyJustReleased,
            KeyPressed,
            KeyReleased,
            ActionJustValidated,
            ActionJustInvalidated,
            ActionValidated,
            ActionInvalidated,
            PadJustPressed,
            PadJustReleased,
            PadPressed,
            PadReleased,
            JoystickAxeValue,
            JoystickAngularValue,
            JoystickTrueNormValue,
            JoystickCorrectedNormValue,
            JoystickJustPressed,
            JoystickJustReleased,
            JoystickPressed,
            JoystickReleased,
            JoystickOrPadJustPressed,
            JoystickOrPadJustReleased,
            JoystickOrPadPressed,
            JoystickOrPadReleased,
            MouseAxeValue,
            MouseAxePosition,
            MouseJustPressed,
            MouseJustReleased,
            MousePressed,
            Sequence,
            SequenceKey,
            SequenceKeyEnd,
            SequencePad,
            SequencePadEnd
        }

        public static FunctionType GetFunctionType(uint index)
        {
            try
            {
                return (FunctionType)(index);
            }
            catch (Exception)
            {
                return FunctionType.Unknown;
            }
        }
    }
}