using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray2Mod.Game.Structs.Input {
    public unsafe class ParsedKeyWord {
        
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

            switch (kw.FunctionType) {
                case InputFunctions.FunctionType.Not:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[0].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    break;

                case InputFunctions.FunctionType.And:
                case InputFunctions.FunctionType.Or:
                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[0].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += subkeywords[1].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                    break;

                case InputFunctions.FunctionType.KeyPressed:
                case InputFunctions.FunctionType.KeyReleased:
                case InputFunctions.FunctionType.KeyJustPressed:
                case InputFunctions.FunctionType.KeyJustReleased:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case InputFunctions.FunctionType.Sequence:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    int sequenceLength = subkeywords[0].kw.indexOrKeyCode;

                    if (sequenceLength > 0) {
                        ParsedKeyWord[] newArray = subkeywords;
                        Array.Resize(ref newArray, sequenceLength + 1);
                        subkeywords = newArray;
                        for (int i = 0; i < sequenceLength; i++) {
                            subkeywords[1 + i] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                            keywordsRead += subkeywords[1 + i].FillInSubKeywords(keywords, thisIndex + keywordsRead);
                        }
                    }
                    break;

                case InputFunctions.FunctionType.SequenceKey:
                case InputFunctions.FunctionType.SequenceKeyEnd:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // Keycode
                    keywordsRead += 1;
                    break;

                case InputFunctions.FunctionType.SequencePad:
                case InputFunctions.FunctionType.SequencePadEnd:

                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // 0
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]); // Keycode
                    keywordsRead += 1;

                    break;

                case InputFunctions.FunctionType.MouseAxeValue:
                case InputFunctions.FunctionType.MouseAxePosition:
                    subkeywords = new ParsedKeyWord[3];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[2] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case InputFunctions.FunctionType.MousePressed:
                case InputFunctions.FunctionType.MouseJustPressed:
                case InputFunctions.FunctionType.MouseJustReleased:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case InputFunctions.FunctionType.PadPressed:
                case InputFunctions.FunctionType.PadReleased:
                case InputFunctions.FunctionType.PadJustPressed:
                case InputFunctions.FunctionType.PadJustReleased:
                case InputFunctions.FunctionType.JoystickPressed:
                case InputFunctions.FunctionType.JoystickReleased:
                case InputFunctions.FunctionType.JoystickJustPressed:
                case InputFunctions.FunctionType.JoystickJustReleased:
                case InputFunctions.FunctionType.JoystickOrPadPressed:
                case InputFunctions.FunctionType.JoystickOrPadReleased:
                case InputFunctions.FunctionType.JoystickOrPadJustPressed:
                case InputFunctions.FunctionType.JoystickOrPadJustReleased:

                    subkeywords = new ParsedKeyWord[2];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    subkeywords[1] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;

                case InputFunctions.FunctionType.JoystickAxeValue:
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

                case InputFunctions.FunctionType.JoystickAngularValue:
                case InputFunctions.FunctionType.JoystickTrueNormValue:
                case InputFunctions.FunctionType.JoystickCorrectedNormValue:
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

                case InputFunctions.FunctionType.ActionValidated:
                case InputFunctions.FunctionType.ActionInvalidated:
                case InputFunctions.FunctionType.ActionJustValidated:
                case InputFunctions.FunctionType.ActionJustInvalidated:
                    subkeywords = new ParsedKeyWord[1];
                    subkeywords[0] = new ParsedKeyWord(keywords[thisIndex + keywordsRead]);
                    keywordsRead += 1;
                    break;
            }
            return keywordsRead;
        }

        public override string ToString()
        {
            if (isFunction) {
                switch (kw.FunctionType) {
                    case InputFunctions.FunctionType.Not:
                        return "!(" + subkeywords[0] + ")";

                    case InputFunctions.FunctionType.And:
                        return "(" + subkeywords[0] + " && " + subkeywords[1] + ")";

                    case InputFunctions.FunctionType.Or:
                        return "(" + subkeywords[0] + " || " + subkeywords[1] + ")";

                    case InputFunctions.FunctionType.KeyPressed:
                    case InputFunctions.FunctionType.KeyReleased:
                    case InputFunctions.FunctionType.KeyJustPressed:
                    case InputFunctions.FunctionType.KeyJustReleased:
                        return kw.FunctionType + "(" + Enum.GetName(typeof(KeyCode), subkeywords[0].kw.indexOrKeyCode) + ")";

                    case InputFunctions.FunctionType.Sequence:
                        string sequence = "";
                        // Skip 1 at the end (first sequenceKey), then do -2 to skip over every other sequenceKey
                        // Then stop because first two keywords (last two processed here) are length and sequenceEnd
                        for (int i = subkeywords.Length - 1; i > 0; i--) {
                            ParsedKeyWord w = subkeywords[i];
                            switch (w.kw.FunctionType) {
                                case InputFunctions.FunctionType.SequenceKey:
                                case InputFunctions.FunctionType.SequenceKeyEnd:
                                    sequence += Enum.GetName(typeof(KeyCode), subkeywords[i].subkeywords[0].kw.indexOrKeyCode);
                                    break;

                                case InputFunctions.FunctionType.SequencePad:
                                case InputFunctions.FunctionType.SequencePadEnd:
                                    sequence += GetJoyPadString(subkeywords[i].subkeywords);
                                    break;
                            }
                        }
                        return "Sequence(\"" + sequence + "\")";

                    case InputFunctions.FunctionType.PadPressed:
                    case InputFunctions.FunctionType.PadReleased:
                    case InputFunctions.FunctionType.PadJustPressed:
                    case InputFunctions.FunctionType.PadJustReleased:
                    case InputFunctions.FunctionType.JoystickPressed:
                    case InputFunctions.FunctionType.JoystickReleased:
                    case InputFunctions.FunctionType.JoystickJustPressed:
                    case InputFunctions.FunctionType.JoystickJustReleased:
                    case InputFunctions.FunctionType.JoystickOrPadPressed:
                    case InputFunctions.FunctionType.JoystickOrPadReleased:
                    case InputFunctions.FunctionType.JoystickOrPadJustPressed:
                    case InputFunctions.FunctionType.JoystickOrPadJustReleased:
                        return kw.FunctionType + GetJoyPadString(subkeywords);

                    case InputFunctions.FunctionType.JoystickAxeValue:
                        return kw.FunctionType + "("
                            + (subkeywords[1].kw.indexOrKeyCode == 4 ? "X" : "Y")
                            + ", " + subkeywords[2].kw.valueAsInt
                            + ", " + subkeywords[3].kw.valueAsInt
                            + (subkeywords[0].kw.indexOrKeyCode != 0 ? (", " + subkeywords[0].kw.indexOrKeyCode) : "") + ")";

                    case InputFunctions.FunctionType.ActionValidated:
                    case InputFunctions.FunctionType.ActionInvalidated:
                    case InputFunctions.FunctionType.ActionJustValidated:
                    case InputFunctions.FunctionType.ActionJustInvalidated:
                        EntryElement* action = subkeywords[0].kw.indexAsPointer;
                        return kw.FunctionType + "{" + (action != null ? action->ToString() : "null") + "}";

                    default:
                        return kw.FunctionType.ToString() + "()";
                }
            } else {
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
