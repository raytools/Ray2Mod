using System;

namespace Ray2Mod.Game.Structs.Input
{
    public unsafe class ParsedKeyWord
    {

        public KeyWord kw;

        public bool isFunction;
        public ParsedKeyWord[] subkeywords;

        public ParsedKeyWord(KeyWord keyword)
        {
            kw = keyword;
        }

        public int FillInSubKeywords(KeyWord[] keywords, int thisIndex)
        {
            isFunction = true;
            int keywordsRead = 1;

            switch (kw.FunctionType)
            {
                case Functions.FunctionType.Not:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[0].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    break;

                case Functions.FunctionType.And:
                case Functions.FunctionType.Or:
                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[0].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[1].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    break;

                case Functions.FunctionType.KeyPressed:
                case Functions.FunctionType.KeyReleased:
                case Functions.FunctionType.KeyJustPressed:
                case Functions.FunctionType.KeyJustReleased:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.Sequence:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    int sequenceLength = subkeywords[0].kw.indexOrKeyCode;

                    if (sequenceLength > 0)
                    {
                        ParsedKeyWord[] newArray = subkeywords;
                        Array.Resize(ref newArray, sequenceLength + 1);
                        subkeywords = newArray;
                        for (int i = 0; i < sequenceLength; i++)
                        {
                            subkeywords[1 + i] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                            keywordsRead += subkeywords[1 + i].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                        }
                    }
                    break;

                case Functions.FunctionType.SequenceKey:
                case Functions.FunctionType.SequenceKeyEnd:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // Keycode
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.SequencePad:
                case Functions.FunctionType.SequencePadEnd:

                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // 0
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // Keycode
                    keywordsRead += 1;

                    break;

                case Functions.FunctionType.MouseAxeValue:
                case Functions.FunctionType.MouseAxePosition:
                    subkeywords = new ParsedKeyWord[3];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[2] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.MousePressed:
                case Functions.FunctionType.MouseJustPressed:
                case Functions.FunctionType.MouseJustReleased:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.PadPressed:
                case Functions.FunctionType.PadReleased:
                case Functions.FunctionType.PadJustPressed:
                case Functions.FunctionType.PadJustReleased:
                case Functions.FunctionType.JoystickPressed:
                case Functions.FunctionType.JoystickReleased:
                case Functions.FunctionType.JoystickJustPressed:
                case Functions.FunctionType.JoystickJustReleased:
                case Functions.FunctionType.JoystickOrPadPressed:
                case Functions.FunctionType.JoystickOrPadReleased:
                case Functions.FunctionType.JoystickOrPadJustPressed:
                case Functions.FunctionType.JoystickOrPadJustReleased:

                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.JoystickAxeValue:
                    subkeywords = new ParsedKeyWord[4];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[2] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[3] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.JoystickAngularValue:
                case Functions.FunctionType.JoystickTrueNormValue:
                case Functions.FunctionType.JoystickCorrectedNormValue:
                    subkeywords = new ParsedKeyWord[5];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[2] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[3] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[4] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case Functions.FunctionType.ActionValidated:
                case Functions.FunctionType.ActionInvalidated:
                case Functions.FunctionType.ActionJustValidated:
                case Functions.FunctionType.ActionJustInvalidated:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;
            }
            return keywordsRead;
        }

        public override string ToString()
        {
            if (isFunction)
            {
                switch (kw.FunctionType)
                {
                    case Functions.FunctionType.Not:
                        return "!(" + subkeywords[0] + ")";

                    case Functions.FunctionType.And:
                        return "(" + subkeywords[0] + " && " + subkeywords[1] + ")";

                    case Functions.FunctionType.Or:
                        return "(" + subkeywords[0] + " || " + subkeywords[1] + ")";

                    case Functions.FunctionType.KeyPressed:
                    case Functions.FunctionType.KeyReleased:
                    case Functions.FunctionType.KeyJustPressed:
                    case Functions.FunctionType.KeyJustReleased:
                        return kw.FunctionType + "(" + Enum.GetName(typeof(KeyCode), subkeywords[0].kw.indexOrKeyCode) + ")";

                    case Functions.FunctionType.Sequence:
                        string sequence = "";
                        // Skip 1 at the end (first sequenceKey), then do -2 to skip over every other sequenceKey
                        // Then stop because first two keywords (last two processed here) are length and sequenceEnd
                        for (int i = subkeywords.Length - 1; i > 0; i--)
                        {
                            ParsedKeyWord w = subkeywords[i];
                            switch (w.kw.FunctionType)
                            {
                                case Functions.FunctionType.SequenceKey:
                                case Functions.FunctionType.SequenceKeyEnd:
                                    sequence += Enum.GetName(typeof(KeyCode), subkeywords[i].subkeywords[0].kw.indexOrKeyCode);
                                    break;

                                case Functions.FunctionType.SequencePad:
                                case Functions.FunctionType.SequencePadEnd:
                                    sequence += GetJoyPadString(subkeywords[i].subkeywords);
                                    break;
                            }
                        }
                        return "Sequence(\"" + sequence + "\")";

                    case Functions.FunctionType.PadPressed:
                    case Functions.FunctionType.PadReleased:
                    case Functions.FunctionType.PadJustPressed:
                    case Functions.FunctionType.PadJustReleased:
                    case Functions.FunctionType.JoystickPressed:
                    case Functions.FunctionType.JoystickReleased:
                    case Functions.FunctionType.JoystickJustPressed:
                    case Functions.FunctionType.JoystickJustReleased:
                    case Functions.FunctionType.JoystickOrPadPressed:
                    case Functions.FunctionType.JoystickOrPadReleased:
                    case Functions.FunctionType.JoystickOrPadJustPressed:
                    case Functions.FunctionType.JoystickOrPadJustReleased:
                        return kw.FunctionType + GetJoyPadString(subkeywords);

                    case Functions.FunctionType.JoystickAxeValue:
                        return kw.FunctionType + "("
                            + (subkeywords[1].kw.indexOrKeyCode == 4 ? "X" : "Y")
                            + ", " + subkeywords[2].kw.valueAsInt
                            + ", " + subkeywords[3].kw.valueAsInt
                            + (subkeywords[0].kw.indexOrKeyCode != 0 ? (", " + subkeywords[0].kw.indexOrKeyCode) : "") + ")";

                    case Functions.FunctionType.ActionValidated:
                    case Functions.FunctionType.ActionInvalidated:
                    case Functions.FunctionType.ActionJustValidated:
                    case Functions.FunctionType.ActionJustInvalidated:
                        EntryAction* action = subkeywords[0].kw.indexAsPointer;
                        return kw.FunctionType + "{" + (action != null ? action->ToString() : "null") + "}";

                    default:
                        return kw.FunctionType.ToString() + "()";
                }
            }
            else
            {
                return "[" + kw.indexOrKeyCode + "]<" + Enum.GetName(typeof(KeyCode), kw.indexOrKeyCode) + ">";
            }
        }

        private string GetJoyPadString(ParsedKeyWord[] subkeywords)
        {
            int firstKW = 0;

            return "(" + Enum.GetName(typeof(JoypadKeyCode), subkeywords[firstKW + 1].kw.indexOrKeyCode) + (subkeywords[firstKW + 0].kw.indexOrKeyCode != 0 ? (", " + subkeywords[firstKW + 0].kw.indexOrKeyCode) : "") + ")";
        }
    }
}
