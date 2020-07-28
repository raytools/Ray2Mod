using System.Runtime.InteropServices;

using Ray2Mod.Structs.Input;

namespace Ray2Mod.Game.Structs.Input
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct InputStructure
    {
        [FieldOffset(0x700)]
        public int numEntryActions;
        [FieldOffset(0x704)]
        public EntryAction* entryActionArray;

        public EntryAction*[] EntryActions
        {
            get
            {
                EntryAction*[] result = new EntryAction*[numEntryActions];
                for (int i = 0; i < numEntryActions; i++)
                {
                    result[i] = &entryActionArray[i];
                }
                return result;
            }
        }

        public EntryAction* GetEntryAction(EntryActionNames name)
        {
            EntryAction*[] actions = EntryActions;
            if (actions.Length > (int)name)
            {
                return actions[(int)name];
            }
            return null;
        }

        public static InputStructure* GetInputStructure()
        {
            return (InputStructure*)Offsets.InputStructure;
        }
    }
}
