using System.Runtime.InteropServices;

namespace Ray2Mod.Game.Structs.Input
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EntryAction
    {
        public int numKeyWords;
        public KeyWord* keywordArray;
        public int name; // Not used in PC?
        public int validCount;
        public int field_10;
        public int field_14;

        public KeyWord[] KeyWords
        {
            get
            {
                KeyWord[] result = new KeyWord[numKeyWords];
                for (int i = 0; i < numKeyWords; i++)
                {
                    result[i] = keywordArray[i];
                }
                return result;
            }
        }

        public ParsedKeyWord ParsedFirstKeyWord
        {
            get
            {
                if (numKeyWords > 0)
                {
                    ParsedKeyWord result = new ParsedKeyWord(KeyWords[0]);
                    result.FillInSubKeywords(KeyWords, 0);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsValidated()
        {
            return validCount > 0;
        }

        public bool IsInvalidated()
        {
            return validCount < 0;
        }

        public bool IsJustValidated()
        {
            return validCount == 1;
        }

        public bool IsJustInvalidated()
        {
            return validCount == -1;
        }

        public override string ToString()
        {
            //string result = "<NullEntryAction>";
            string result = "EntryAction{";

            if (ParsedFirstKeyWord != null)
            {
                result += ParsedFirstKeyWord.ToString() + ", ";
            }
            if (result.EndsWith("("))
            {
                result = "<NullEntryAction>";
            }
            else if (result.EndsWith(", "))
            {
                result = result.Substring(0, result.Length - 2) + "}";
            }
            else
            {
                result += "}";
            }
            return result;
        }
    }
}