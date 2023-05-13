using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using HtmlTextExtractor.Enums;

namespace HtmlTextExtractor.Services;

public class HtmlToPlainTextConverter
{
    /// <summary>
    /// Extract and clean up text from HTML code
    /// </summary>
    /// <param name="htmlText"></param>
    /// <param name="preserveTags"></param>
    /// <returns></returns>
    public static string ExtractHtmlText(string htmlText, bool preserveTags)
    {
        var stringBuilder = new StringBuilder();
        var flag1 = false;
        var flag2 = false;
        var flag3 = false;
        var flag4 = false;
        var flag5 = false;
        var flag6 = false;
        var flag7 = false;
        var num1 = 0;
        var indexA = 0;
        var flag8 = false;
        var flag9 = false;
        var num2 = 0;
        var flag10 = false;
        var ch = ' ';
        var index1 = 0;
        var lower = htmlText.ToLower();
        var startIndex1 = lower.IndexOf("<head", StringComparison.OrdinalIgnoreCase);
        if (startIndex1 > -1)
        {
            var index2 = startIndex1 + "<head".Length;
            if (lower.Length > index2 && (lower[index2] == '>' || char.IsWhiteSpace(lower[index2])))
            {
                var num3 = startIndex1;
                startIndex1 = lower.IndexOf("</head", startIndex1 + 5, StringComparison.OrdinalIgnoreCase);
                if (startIndex1 > -1)
                {
                    var num4 = startIndex1 + "</head".Length;
                    if (lower.Length > num4 && (lower[num4] == '>' || char.IsWhiteSpace(lower[num4])))
                    {
                        var num5 = lower.IndexOf('>', num4);
                        if (num5 <= -1)
                            return string.Empty;
                        startIndex1 = num5 + 1;
                    }
                    else
                        startIndex1 = num3;
                }
            }
            else
                startIndex1 = 0;
        }

        if (startIndex1 < 0)
            startIndex1 = 0;
        var strA = htmlText.Substring(startIndex1);
        var length = strA.Length;
        var index3 = 0;
        var startIndex2 = 0;
        while (index3 <= length)
        {
            if (index3 < length)
            {
                if (flag1)
                {
                    if (flag5)
                    {
                        if (strA[index3] == '>' && strA[index3 - 1] == '-' && strA[index3 - 2] == '-')
                        {
                            flag5 = false;
                        }
                        else
                        {
                            ++index3;
                            continue;
                        }
                    }

                    if (flag4 && index3 < length - 1 && strA[index3] == '-' && strA[index3 + 1] == '-')
                    {
                        flag4 = false;
                        flag5 = true;
                        index3 += 2;
                        continue;
                    }

                    if (flag6 && strA[index3] == '"')
                        flag6 = false;
                    else if (flag7 && strA[index3] == '\'')
                    {
                        flag7 = false;
                    }
                    else
                    {
                        switch (strA[index3])
                        {
                            case '"':
                                flag6 = true;
                                break;
                            case '\'':
                                flag7 = true;
                                break;
                            case '>':
                                flag1 = false;
                                var num6 = index3 + 1;
                                var flag11 = true;
                                var index4 = indexA - 1;
                                do
                                {
                                } while (++index4 < num6 && IsAlphanumeric(strA[index4]));

                                var num7 = index4 - indexA;
                                if (preserveTags &&
                                    (num7 == 1 && string.Compare(strA, indexA, "a", 0, 1,
                                        StringComparison.OrdinalIgnoreCase) == 0 || num7 == 3 && string.Compare(strA,
                                        indexA, "img", 0, 3, StringComparison.OrdinalIgnoreCase) == 0))
                                {
                                    stringBuilder.Append(strA.Substring(startIndex2, num6 - startIndex2));
                                    flag11 = false;
                                }

                                if (flag11)
                                    stringBuilder.Append(strA.Substring(startIndex2, num1 - startIndex2));
                                startIndex2 = num6;
                                if (flag3 && num7 == 2 && string.Compare(strA, indexA, "br", 0, 2,
                                        StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    if (flag9)
                                    {
                                        flag9 = false;
                                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                                    }

                                    stringBuilder.Append("\r\n");
                                    if (num2 > 0)
                                        --num2;
                                    flag10 = false;
                                    flag8 = false;
                                    break;
                                }

                                if (num7 == 1 && string.Compare(strA, indexA, "p", 0, 1,
                                        StringComparison.OrdinalIgnoreCase) == 0 ||
                                    num7 == 5 && string.Compare(strA, indexA, "table", 0, 5,
                                        StringComparison.OrdinalIgnoreCase) == 0 ||
                                    num7 == 2 && string.Compare(strA, indexA, "tr", 0, 2,
                                        StringComparison.OrdinalIgnoreCase) == 0 ||
                                    num7 == 3 && string.Compare(strA, indexA, "div", 0, 3,
                                        StringComparison.OrdinalIgnoreCase) == 0 ||
                                    num7 == 2 && string.Compare(strA, indexA, "li", 0, 2,
                                        StringComparison.OrdinalIgnoreCase) == 0 ||
                                    num7 == 2 && string.Compare(strA, indexA, "ul", 0, 2,
                                        StringComparison.OrdinalIgnoreCase) == 0 || num7 == 2 && string.Compare(strA,
                                        indexA, "ol", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    if (flag9)
                                    {
                                        flag9 = false;
                                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                                    }

                                    if (num7 == 1 && string.Compare(strA, indexA, "p", 0, 1,
                                            StringComparison.OrdinalIgnoreCase) == 0)
                                    {
                                        if (flag10)
                                            ++num2;
                                        flag10 = false;
                                    }
                                    else
                                    {
                                        num2 /= 2;
                                        flag10 = true;
                                    }

                                    for (var index5 = 0; index5 < num2; ++index5)
                                        stringBuilder.Append(" \r\n ");
                                    num2 = 0;
                                    flag8 = false;
                                    break;
                                }

                                flag10 = false;
                                break;
                        }
                    }

                    if (!flag1)
                    {
                        ++index3;
                        continue;
                    }
                }
                else if (flag2)
                {
                    if (!IsAlphanumeric(strA[index3]) && strA[index3] != '!' && strA[index3] != '?' &&
                        strA[index3] != '/')
                    {
                        flag9 = false;
                        flag8 = true;
                        num2 = 2;
                        flag10 = false;
                    }
                    else
                    {
                        flag3 = strA[index3] != '/';
                        flag4 = strA[index3] == '!';
                        flag1 = true;
                        num1 = index3 - 1;
                        indexA = index3 + (flag3 ? 0 : 1);
                    }

                    flag2 = false;
                }
                else if (strA[index3] == '<')
                    flag2 = true;
            }

            if (!flag1 && !flag2 && index3 < length)
            {
                bool flag12;
                if (strA[index3] == '&')
                {
                    if (!preserveTags && length - index3 > 2)
                    {
                        if (strA[index3 + 1] == '#')
                        {
                            index1 = index3 + 1;
                            var flag13 = false;
                            var num8 = 2;
                            if (index1 + 1 < length && char.ToLower(strA[index1 + 1]) == 'x')
                            {
                                flag13 = true;
                                ++index1;
                                num8 = 3;
                            }

                            do
                            {
                            } while (index1 < strA.Length - 1 && (!flag13 && IsNumeric(strA[++index1]) ||
                                                                  flag13 && IsAlphanumeric(strA[++index1])));

                            var num9 = 32;
                            try
                            {
                                if (strA.Length > index3 + num8 && index1 - (index3 + num8) > 0)
                                    num9 = Convert.ToInt32(strA.Substring(index3 + num8, index1 - (index3 + num8)),
                                        flag13 ? 16 : 10);
                                if (num9 >= 0)
                                {
                                    if (num9 <= ushort.MaxValue)
                                        goto label_231;
                                }

                                num9 = 32;
                            }
                            catch (FormatException)
                            {
                            }

                            label_231:
                            ch = Convert.ToChar(num9);
                        }
                        else if (string.Compare(strA, index3 + 1, "quot", 0, 4, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = '"';
                            index1 = index3 + 5;
                        }
                        else if (string.Compare(strA, index3 + 1, "amp", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ch = '&';
                            index1 = index3 + 4;
                        }
                        else if (string.Compare(strA, index3 + 1, "lt", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ch = '<';
                            index1 = index3 + 3;
                        }
                        else if (string.Compare(strA, index3 + 1, "gt", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ch = '>';
                            index1 = index3 + 3;
                        }
                        else if (string.Compare(strA, index3 + 1, "nbsp", 0, 4, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = ' ';
                            index1 = index3 + 5;
                        }
                        else if (string.Compare(strA, index3 + 1, "iexcl", 0, 5, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = Convert.ToChar(161);
                            index1 = index3 + 6;
                        }
                        else if (string.Compare(strA, index3 + 1, "cent", 0, 4, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = Convert.ToChar(162);
                            index1 = index3 + 5;
                        }
                        else if (string.Compare(strA, index3 + 1, "pound", 0, 5, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = Convert.ToChar(163);
                            index1 = index3 + 6;
                        }
                        else if (string.Compare(strA, index3 + 1, "curren", 0, 6, StringComparison.OrdinalIgnoreCase) ==
                                 0)
                        {
                            ch = Convert.ToChar(164);
                            index1 = index3 + 7;
                        }
                        else if (string.Compare(strA, index3 + 1, "yen", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ch = Convert.ToChar(165);
                            index1 = index3 + 4;
                        }
                        else if (string.Compare(strA, index3 + 1, "brvbar", 0, 6, StringComparison.OrdinalIgnoreCase) !=
                                 0 && string.Compare(strA, index3 + 1, "brkbar", 0, 6,
                                     StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            if (string.Compare(strA, index3 + 1, "sect", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                ch = '§';
                                index1 = index3 + 5;
                            }
                            else if (string.Compare(strA, index3 + 1, "uml", 0, 3,
                                         StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(strA, index3 + 1,
                                         "dir", 0, 3, StringComparison.OrdinalIgnoreCase) != 0)
                            {
                                if (string.Compare(strA, index3 + 1, "copy", 0, 4,
                                        StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(169);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "ordf", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(170);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "laquo", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = '«';
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "not", 0, 3,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(172);
                                    index1 = index3 + 4;
                                }
                                else if (string.Compare(strA, index3 + 1, "shy", 0, 3,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = '-';
                                    index1 = index3 + 4;
                                }
                                else if (string.Compare(strA, index3 + 1, "reg", 0, 3,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = '®';
                                    index1 = index3 + 4;
                                }
                                else if (string.Compare(strA, index3 + 1, "macr", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(175);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "hibar", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(175);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "deg", 0, 3,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(176);
                                    index1 = index3 + 4;
                                }
                                else if (string.Compare(strA, index3 + 1, "plusmn", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(177);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "sup2", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(178);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "sup3", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(179);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "acute", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(180);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "micro", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(181);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "para", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(182);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "middot", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(183);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "cedil", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(184);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "sup1", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(185);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "ordm", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(186);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "raquo", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(187);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "frac14", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(188);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "frac12", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(189);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "frac34", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(190);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "iquest", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(191);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "times", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(215);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "szlig", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(223);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "agrave", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(224) : Convert.ToChar(192);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "aacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(225) : Convert.ToChar(193);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "acirc", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(226) : Convert.ToChar(194);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "atilde", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(227) : Convert.ToChar(195);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "auml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(228) : Convert.ToChar(196);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "aring", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(229) : Convert.ToChar(197);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "aelig", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'a' ? Convert.ToChar(230) : Convert.ToChar(198);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "ccedil", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'c' ? Convert.ToChar(231) : Convert.ToChar(199);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "egrave", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'e' ? Convert.ToChar(232) : Convert.ToChar(200);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "eacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'e' ? Convert.ToChar(233) : Convert.ToChar(201);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ecirc", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'e' ? Convert.ToChar(234) : Convert.ToChar(202);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "euml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'e' ? Convert.ToChar(235) : Convert.ToChar(203);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "igrave", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'i' ? Convert.ToChar(236) : Convert.ToChar(204);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "iacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'i' ? Convert.ToChar(237) : Convert.ToChar(205);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "icirc", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'i' ? Convert.ToChar(238) : Convert.ToChar(206);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "iuml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'i' ? Convert.ToChar(239) : Convert.ToChar(207);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "eth", 0, 3,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'e' ? Convert.ToChar(240) : Convert.ToChar(208);
                                    index1 = index3 + 4;
                                }
                                else if (string.Compare(strA, index3 + 1, "ntilde", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'n' ? Convert.ToChar(241) : Convert.ToChar(209);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ograve", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(242) : Convert.ToChar(210);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "oacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(243) : Convert.ToChar(211);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ocirc", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(244) : Convert.ToChar(212);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "otilde", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(245) : Convert.ToChar(213);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ouml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(246) : Convert.ToChar(214);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "divide", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar(247);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "oslash", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'o' ? Convert.ToChar(248) : Convert.ToChar(216);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ugrave", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'u' ? Convert.ToChar(249) : Convert.ToChar(217);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "uacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'u' ? Convert.ToChar(250) : Convert.ToChar(218);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "ucirc", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'u' ? Convert.ToChar(251) : Convert.ToChar(219);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "uuml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'u' ? Convert.ToChar(252) : Convert.ToChar(220);
                                    index1 = index3 + 5;
                                }
                                else if (string.Compare(strA, index3 + 1, "yacute", 0, 6,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 'y' ? Convert.ToChar(253) : Convert.ToChar(221);
                                    index1 = index3 + 7;
                                }
                                else if (string.Compare(strA, index3 + 1, "thorn", 0, 5,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = strA[index3 + 1] == 't' ? Convert.ToChar(254) : Convert.ToChar(222);
                                    index1 = index3 + 6;
                                }
                                else if (string.Compare(strA, index3 + 1, "yuml", 0, 4,
                                             StringComparison.OrdinalIgnoreCase) == 0)
                                {
                                    ch = Convert.ToChar((int)byte.MaxValue);
                                    index1 = index3 + 5;
                                }
                            }
                            else
                            {
                                ch = Convert.ToChar(168);
                                index1 = index3 + 4;
                            }
                        }
                        else
                        {
                            ch = Convert.ToChar(166);
                            index1 = index3 + 7;
                        }

                        if (index1 < length && strA[index1] == ';')
                            ++index1;
                    }

                    flag12 = true;
                    flag9 = false;
                    flag8 = true;
                    num2 = 2;
                    flag10 = false;
                }
                else
                {
                    index1 = index3 - 1;
                    do
                    {
                    } while (++index1 < length && IsWhiteSpace(strA[index1]));

                    if (index1 > index3 & flag8)
                        flag9 = true;
                    ch = ' ';
                    flag12 = false;
                }

                if (index1 > index3)
                {
                    stringBuilder.Append(strA.Substring(startIndex2, index3 - startIndex2));
                    startIndex2 = index1;
                    index3 = index1 - 1;
                    if (flag12 | flag9)
                        stringBuilder.Append(ch);
                }
                else
                {
                    flag9 = false;
                    flag8 = true;
                    num2 = 2;
                    flag10 = false;
                }
            }

            ++index3;
        }

        var num10 = index3 - 1;
        stringBuilder.Append(strA.Substring(startIndex2, num10 - startIndex2));
        return stringBuilder.ToString();
    }

    public static bool IsWhiteSpace(char c) => c is ' ' or '\t' or '\f';

    public static bool IsNumeric(char c) => c is >= '0' and <= '9';

    private static bool IsAlphanumeric(char c)
    {
        if (c >= '0' && c <= '9' || c >= 'a' && c <= 'z')
            return true;
        return c >= 'A' && c <= 'Z';
    }

    private static string ExtractHrefText(string input)
    {
        var str1 = string.Empty;
        char[] chArray = { '<', '>' };
        input = input.Trim(chArray);
        var match = RegularExpressions.RegexHrefSrcText.Match(input);
        var str2 = match.Groups["srcText"].Value.Trim(' ', '\'', '"');
        if (str2.Length != 0)
            str1 = string.Format(CultureInfo.InvariantCulture, "[{0}]", str2);
        return str1;
    }

    private static string ExtractImgAltTextAndUri(
        string input,
        HtmlToPlainConvertOptions options)
    {
        var chArray1 = new[] { '<', '>' };
        input = input.Trim(chArray1);
        var str1 = RegularExpressions.RegexAltText.Match(input).Groups["altText"].Value;
        var str2 = RegularExpressions.RegexSrcText.Match(input).Groups["srcText"].Value;
        var chArray2 = new[] { ' ', '\'', '"' };
        var str3 = str2.Trim(chArray2);
        var str4 = str1.Trim(chArray2);
        if ((options & HtmlToPlainConvertOptions.AddImgAltText) != HtmlToPlainConvertOptions.AddImgAltText &&
            str4.Length != 0)
            str4 = string.Empty;
        if ((options & HtmlToPlainConvertOptions.WriteImageIfNoAlt) == HtmlToPlainConvertOptions.WriteImageIfNoAlt &&
            str4.Length == 0)
            str4 = "image";
        if ((options & HtmlToPlainConvertOptions.AddUriForImg) != HtmlToPlainConvertOptions.AddUriForImg &&
            str3.Length != 0)
            str3 = string.Empty;
        string str5;
        if (str4.Length != 0 && str3.Length != 0)
            str5 = string.Format(CultureInfo.InvariantCulture, "[{0} {1}]", str4, str3);
        else if (str4.Length != 0)
            str5 = string.Format(CultureInfo.InvariantCulture, "[{0}]", str4);
        else if (str3.Length != 0)
            str5 = string.Format(CultureInfo.InvariantCulture, "[{0}]", str3);
        else
            str5 = string.Empty;
        return str5;
    }

    public static string ConvertHtmlToPlainText(
        string srcInput,
        HtmlToPlainConvertOptions options,
        bool decodeHtmlEntities)
    {
        var match1 = RegularExpressions.RegexBody.Match(srcInput);
        if (match1.Success)
        {
            var index = match1.Index;
            var match2 = match1.NextMatch();
            var num = !match2.Success ? srcInput.Length - 1 : match2.Index;
            srcInput = srcInput.Substring(index, num - index);
        }

        var stringBuilder = new StringBuilder();
        var startIndex = 0;
        for (var match3 = RegularExpressions.RegexPre.Match(srcInput); match3.Success; match3 = match3.NextMatch())
        {
            var input = srcInput.Substring(startIndex, match3.Index - startIndex);
            var regex17 = RegularExpressions.RegexWhiteSpace;
            stringBuilder.Append(regex17.Replace(input, " "));
            startIndex = match3.Index + match3.Length;
            stringBuilder.Append(match3.Value);
        }

        if (startIndex < srcInput.Length)
        {
            var input = srcInput.Substring(startIndex);
            var regex17 = RegularExpressions.RegexWhiteSpace;
            stringBuilder.Append(regex17.Replace(input, " "));
        }

        var string01 = stringBuilder.ToString();
        if ((options & HtmlToPlainConvertOptions.AddUriForAHRef) == HtmlToPlainConvertOptions.AddUriForAHRef)
            string01 = AddHrefUri(string01);
        if ((options & HtmlToPlainConvertOptions.AddImgAltText) == HtmlToPlainConvertOptions.AddImgAltText ||
            (options & HtmlToPlainConvertOptions.AddUriForImg) == HtmlToPlainConvertOptions.AddUriForImg ||
            (options & HtmlToPlainConvertOptions.WriteImageIfNoAlt) == HtmlToPlainConvertOptions.WriteImageIfNoAlt)
            string01 = AddImgAltTextAndUri(string01, options);
        return HtmlUrlEncoder.HtmlDecode(
            ExtractHtmlText(RemoveScriptTags(RemoveStyleTags(RemoveHtmlComments(string01))), decodeHtmlEntities));
    }

    private static string RemoveHtmlComments(string input)
    {
        var startIndex = 0;
        while (true)
        {
            startIndex = input.IndexOf("<!--", startIndex, StringComparison.OrdinalIgnoreCase);
            if (startIndex != -1)
            {
                var num = input.IndexOf("-->", startIndex, StringComparison.OrdinalIgnoreCase);
                if (num != -1)
                    input = input.Remove(startIndex, num + 3 - startIndex);
                else
                    break;
            }
            else
                break;
        }

        return input;
    }

    private static string RemoveStyleTags(string input)
    {
        Match match;
        do
        {
            match = RegularExpressions.RegexStyle.Match(input);
            if (match.Success)
                goto label_2;
            label_1:
            continue;
            label_2:
            input = input.Remove(match.Index, match.Length);
            goto label_1;
        } while (match.Success);

        return input;
    }

    private static string RemoveScriptTags(string input)
    {
        Match match;
        do
        {
            match = RegularExpressions.RegexScript.Match(input);
            if (match.Success)
                goto label_2;
            label_1:
            continue;
            label_2:
            input = input.Remove(match.Index, match.Length);
            goto label_1;
        } while (match.Success);

        return input;
    }

    private static string AddHrefUri(string input)
    {
        for (var match = RegularExpressions.RegexA.Match(input); match.Success; match = match.NextMatch())
        {
            var stringCollection = HtmlTagExtractor.ExtractHtmlTags("a", match.Value, true);
            if (stringCollection.Count > 0)
            {
                var str = ExtractHrefText(stringCollection[0]);
                var newValue = string.Format("{0} {1}", match.Groups["tagContent"].Value, str);
                input = input.Replace(match.Value, newValue);
            }
        }

        return input;
    }

    private static string AddImgAltTextAndUri(
        string input,
        HtmlToPlainConvertOptions options)
    {
        foreach (var str in HtmlTagExtractor.ExtractHtmlTags("img", input, true))
        {
            var newValue = ExtractImgAltTextAndUri(str, options);
            input = input.Replace(str, newValue);
        }

        return input;
    }
}
