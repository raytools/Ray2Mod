using System.Collections.Generic;
using System.Text;

using Ray2Mod.Game;

namespace Ray2Mod.Utils
{
    // TODO: Check if still necessary, World.cs does this now, maybe move it back here?
    public static unsafe class ObjectNameUtils
    {
        public const int off_objectTypes = Offsets.ObjectTypes;

        public static Dictionary<int, string[]> ReadObjectTypes()
        {
            Dictionary<int, string[]> objectTypes = new Dictionary<int, string[]>();
            for (int i = 0; i < 3; i++)
            {
                int* off_names_header = (int*)(off_objectTypes + (i * 12));
                int off_names_first = *off_names_header;
                int off_names_last = *(off_names_header + 1);
                int num_names = *(off_names_header + 2);

                objectTypes[i] = ReadObjectNamesTable((int*)off_names_first, num_names);
            }

            return objectTypes;
        }

        public static string[] ReadObjectNamesTable(int* off_names_first, int num_names)
        {
            int* currentOffset = off_names_first;
            string[] names = new string[num_names];

            for (int j = 0; j < num_names; j++)
            {
                int* off_names_next = (int*)*currentOffset;
                byte* off_name = (byte*)*(currentOffset + 3);

                byte[] nameBytes = new byte[64];
                for (int i = 0; i < nameBytes.Length && off_name[i] != 0; i++)
                {
                    nameBytes[i] = off_name[i];
                }

                names[j] = Encoding.GetEncoding(1252).GetString(nameBytes).Trim('\0');

                if (off_names_next != null)
                {
                    currentOffset = off_names_next;
                }
            }

            return names;
        }
    }
}