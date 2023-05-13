using System.Collections;
using System.Globalization;
using System.Text;

namespace HtmlTextExtractor.Services;

/// <summary>
/// The class is used for HTML character encoding/decoding,
/// URL encoding/decoding and similar operations related to
/// HTML and URL manipulation. It includes methods
/// that convert special HTML characters to their equivalent ASCII characters
/// and vice versa, methods that handle URL encoding and decoding,
/// and similar operations.
/// </summary>
internal class HtmlUrlEncoder
{
    private static readonly Hashtable SpecialHtmlCharacters = new()
    {
        { "nbsp", ' ' },
        { "iexcl", '¡' },
        { "cent", '¢' },
        { "pound", '£' },
        { "curren", '¤' },
        { "yen", '¥' },
        { "brvbar", '¦' },
        { "sect", '§' },
        { "uml", '¨' },
        { "copy", '©' },
        { "ordf", 'ª' },
        { "laquo", '«' },
        { "not", '¬' },
        { "shy", '\u00AD' },
        { "reg", '®' },
        { "macr", '¯' },
        { "deg", '°' },
        { "plusmn", '±' },
        { "sup2", '\u00B2' },
        { "sup3", '\u00B3' },
        { "acute", '´' },
        { "micro", 'µ' },
        { "para", '¶' },
        { "middot", '·' },
        { "cedil", '¸' },
        { "sup1", '\u00B9' },
        { "ordm", 'º' },
        { "raquo", '»' },
        { "frac14", '\u00BC' },
        { "frac12", '\u00BD' },
        { "frac34", '\u00BE' },
        { "iquest", '¿' },
        { "Agrave", 'À' },
        { "Aacute", 'Á' },
        { "Acirc", 'Â' },
        { "Atilde", 'Ã' },
        { "Auml", 'Ä' },
        { "Aring", 'Å' },
        { "AElig", 'Æ' },
        { "Ccedil", 'Ç' },
        { "Egrave", 'È' },
        { "Eacute", 'É' },
        { "Ecirc", 'Ê' },
        { "Euml", 'Ë' },
        { "Igrave", 'Ì' },
        { "Iacute", 'Í' },
        { "Icirc", 'Î' },
        { "Iuml", 'Ï' },
        { "ETH", 'Ð' },
        { "Ntilde", 'Ñ' },
        { "Ograve", 'Ò' },
        { "Oacute", 'Ó' },
        { "Ocirc", 'Ô' },
        { "Otilde", 'Õ' },
        { "Ouml", 'Ö' },
        { "times", '×' },
        { "Oslash", 'Ø' },
        { "Ugrave", 'Ù' },
        { "Uacute", 'Ú' },
        { "Ucirc", 'Û' },
        { "Uuml", 'Ü' },
        { "Yacute", 'Ý' },
        { "THORN", 'Þ' },
        { "szlig", 'ß' },
        { "agrave", 'à' },
        { "aacute", 'á' },
        { "acirc", 'â' },
        { "atilde", 'ã' },
        { "auml", 'ä' },
        { "aring", 'å' },
        { "aelig", 'æ' },
        { "ccedil", 'ç' },
        { "egrave", 'è' },
        { "eacute", 'é' },
        { "ecirc", 'ê' },
        { "euml", 'ë' },
        { "igrave", 'ì' },
        { "iacute", 'í' },
        { "icirc", 'î' },
        { "iuml", 'ï' },
        { "eth", 'ð' },
        { "ntilde", 'ñ' },
        { "ograve", 'ò' },
        { "oacute", 'ó' },
        { "ocirc", 'ô' },
        { "otilde", 'õ' },
        { "ouml", 'ö' },
        { "divide", '÷' },
        { "oslash", 'ø' },
        { "ugrave", 'ù' },
        { "uacute", 'ú' },
        { "ucirc", 'û' },
        { "uuml", 'ü' },
        { "yacute", 'ý' },
        { "thorn", 'þ' },
        { "yuml", 'ÿ' },
        { "fnof", 'ƒ' },
        { "Alpha", 'Α' },
        { "Beta", 'Β' },
        { "Gamma", 'Γ' },
        { "Delta", 'Δ' },
        { "Epsilon", 'Ε' },
        { "Zeta", 'Ζ' },
        { "Eta", 'Η' },
        { "Theta", 'Θ' },
        { "Iota", 'Ι' },
        { "Kappa", 'Κ' },
        { "Lambda", 'Λ' },
        { "Mu", 'Μ' },
        { "Nu", 'Ν' },
        { "Xi", 'Ξ' },
        { "Omicron", 'Ο' },
        { "Pi", 'Π' },
        { "Rho", 'Ρ' },
        { "Sigma", 'Σ' },
        { "Tau", 'Τ' },
        { "Upsilon", 'Υ' },
        { "Phi", 'Φ' },
        { "Chi", 'Χ' },
        { "Psi", 'Ψ' },
        { "Omega", 'Ω' },
        { "alpha", 'α' },
        { "beta", 'β' },
        { "gamma", 'γ' },
        { "delta", 'δ' },
        { "epsilon", 'ε' },
        { "zeta", 'ζ' },
        { "eta", 'η' },
        { "theta", 'θ' },
        { "iota", 'ι' },
        { "kappa", 'κ' },
        { "lambda", 'λ' },
        { "mu", 'μ' },
        { "nu", 'ν' },
        { "xi", 'ξ' },
        { "omicron", 'ο' },
        { "pi", 'π' },
        { "rho", 'ρ' },
        { "sigmaf", 'ς' },
        { "sigma", 'σ' },
        { "tau", 'τ' },
        { "upsilon", 'υ' },
        { "phi", 'φ' },
        { "chi", 'χ' },
        { "psi", 'ψ' },
        { "omega", 'ω' },
        { "thetasym", 'ϑ' },
        { "upsih", 'ϒ' },
        { "piv", 'ϖ' },
        { "bull", '•' },
        { "hellip", '…' },
        { "prime", '′' },
        { "Prime", '″' },
        { "oline", '‾' },
        { "frasl", '⁄' },
        { "weierp", '℘' },
        { "image", 'ℑ' },
        { "real", 'ℜ' },
        { "trade", '™' },
        { "alefsym", 'ℵ' },
        { "larr", '←' },
        { "uarr", '↑' },
        { "rarr", '→' },
        { "darr", '↓' },
        { "harr", '↔' },
        { "crarr", '↵' },
        { "lArr", '⇐' },
        { "uArr", '⇑' },
        { "rArr", '⇒' },
        { "dArr", '⇓' },
        { "hArr", '⇔' },
        { "forall", '∀' },
        { "part", '∂' },
        { "exist", '∃' },
        { "empty", '∅' },
        { "nabla", '∇' },
        { "isin", '∈' },
        { "notin", '∉' },
        { "ni", '∋' },
        { "prod", '∏' },
        { "sum", '∑' },
        { "minus", '−' },
        { "lowast", '∗' },
        { "radic", '√' },
        { "prop", '∝' },
        { "infin", '∞' },
        { "ang", '∠' },
        { "and", '∧' },
        { "or", '∨' },
        { "cap", '∩' },
        { "cup", '∪' },
        { "int", '∫' },
        { "there4", '∴' },
        { "sim", '∼' },
        { "cong", '≅' },
        { "asymp", '≈' },
        { "ne", '≠' },
        { "equiv", '≡' },
        { "le", '≤' },
        { "ge", '≥' },
        { "sub", '⊂' },
        { "sup", '⊃' },
        { "nsub", '⊄' },
        { "sube", '⊆' },
        { "supe", '⊇' },
        { "oplus", '⊕' },
        { "otimes", '⊗' },
        { "perp", '⊥' },
        { "sdot", '⋅' },
        { "lceil", '⌈' },
        { "rceil", '⌉' },
        { "lfloor", '⌊' },
        { "rfloor", '⌋' },
        { "lang", '〈' },
        { "rang", '〉' },
        { "loz", '◊' },
        { "spades", '♠' },
        { "clubs", '♣' },
        { "hearts", '♥' },
        { "diams", '♦' },
        { "quot", '"' },
        { "amp", '&' },
        { "lt", '<' },
        { "gt", '>' },
        { "OElig", 'Œ' },
        { "oelig", 'œ' },
        { "Scaron", 'Š' },
        { "scaron", 'š' },
        { "Yuml", 'Ÿ' },
        { "circ", 'ˆ' },
        { "tilde", '˜' },
        { "ensp", ' ' },
        { "emsp", ' ' },
        { "thinsp", ' ' },
        { "zwnj", '\u200C' },
        { "zwj", '\u200D' },
        { "lrm", '\u200E' },
        { "rlm", '\u200F' },
        { "ndash", '–' },
        { "mdash", '—' },
        { "lsquo", '‘' },
        { "rsquo", '’' },
        { "sbquo", '‚' },
        { "ldquo", '“' },
        { "rdquo", '”' },
        { "bdquo", '„' },
        { "dagger", '†' },
        { "Dagger", '‡' },
        { "permil", '‰' },
        { "lsaquo", '‹' },
        { "rsaquo", '›' },
        { "euro", '€' },
    };

    private static readonly char[] HexDigits = "0123456789abcdef".ToCharArray();

    private static int GetHexValue(byte input)
    {
        var ch = (char)input;
        if (ch >= '0' && ch <= '9')
            return ch - 48;
        if (ch >= 'a' && ch <= 'f')
            return ch - 97 + 10;
        return ch >= 'A' && ch <= 'F' ? ch - 65 + 10 : -1;
    }

    private static int GetHexValueFromBytes(byte[] input, int offset, int count)
    {
        var num1 = 0;
        var num2 = count + offset;
        for (var index = offset; index < num2; ++index)
        {
            var num3 = GetHexValue(input[index]);
            if (num3 == -1)
                return -1;
            num1 = (num1 << 4) + num3;
        }

        return num1;
    }

    public static byte[] UrlDecode(byte[] input, int offset, int count)
    {
        if (input == null)
            return null;
        if (count == 0)
            return new byte[0];
        var length = input.Length;
        if (offset < 0 || offset >= length)
            throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0 || offset > length - count)
            throw new ArgumentOutOfRangeException(nameof(count));
        var memoryStream = new MemoryStream();
        var num1 = offset + count;
        for (var index = offset; index < num1; ++index)
        {
            var ch = (char)input[index];
            switch (ch)
            {
                case '%':
                    if (index < num1 - 2)
                    {
                        var num2 = GetHexValueFromBytes(input, index + 1, 2);
                        if (num2 != -1)
                        {
                            ch = (char)num2;
                            index += 2;
                        }
                    }

                    break;
                case '+':
                    ch = ' ';
                    break;
            }

            memoryStream.WriteByte((byte)ch);
        }

        return memoryStream.ToArray();
    }

    public static string UrlEncode(string input, Encoding encoding)
    {
        switch (input)
        {
            case null:
                return null;
            case "":
                return "";
            default:
                var flag = false;
                var length = input.Length;
                for (var index = 0; index < length; ++index)
                {
                    var c = input[index];
                    if ((c < '0' || c < 'A' && c > '9' || c > 'Z' && c < 'a' || c > 'z') && !IsUnreservedChar(c))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    return input;
                var numArray = new byte[encoding.GetMaxByteCount(input.Length)];
                var bytes1 = encoding.GetBytes(input, 0, input.Length, numArray, 0);
                var bytes2 = UrlEncode(numArray, 0, bytes1);
                return Encoding.ASCII.GetString(bytes2, 0, bytes2.Length);
        }
    }

    private static bool IsUnreservedChar(char c) => c is '!' or '\'' or '(' or ')' or '*' or '-' or '.' or '_';

    private static void WriteEncodedCharToStream(char c, Stream stream, bool isUnicode)
    {
        if (c > 'ÿ')
        {
            int num = c;
            stream.WriteByte(37);
            stream.WriteByte(117);
            var index1 = num >> 12;
            stream.WriteByte((byte)HexDigits[index1]);
            var index2 = num >> 8 & 15;
            stream.WriteByte((byte)HexDigits[index2]);
            var index3 = num >> 4 & 15;
            stream.WriteByte((byte)HexDigits[index3]);
            var index4 = num & 15;
            stream.WriteByte((byte)HexDigits[index4]);
        }
        else if (c > ' ' && IsUnreservedChar(c))
            stream.WriteByte((byte)c);
        else if (c == ' ')
            stream.WriteByte(43);
        else if (c >= '0' && (c >= 'A' || c <= '9') && (c <= 'Z' || c >= 'a') && c <= 'z')
        {
            stream.WriteByte((byte)c);
        }
        else
        {
            if (isUnicode && c > '\u007F')
            {
                stream.WriteByte(37);
                stream.WriteByte(117);
                stream.WriteByte(48);
                stream.WriteByte(48);
            }
            else
                stream.WriteByte(37);

            var index5 = c >> 4;
            stream.WriteByte((byte)HexDigits[index5]);
            var index6 = c & 15;
            stream.WriteByte((byte)HexDigits[index6]);
        }
    }

    public static byte[] UrlEncode(byte[] input, int offset, int count)
    {
        if (input == null)
            return null;
        var length = input.Length;
        if (length == 0)
            return Array.Empty<byte>();
        if (offset < 0 || offset >= length)
            throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0 || count > length - offset)
            throw new ArgumentOutOfRangeException(nameof(count));
        var memoryStream = new MemoryStream(count);
        var num = offset + count;
        for (var index = offset; index < num; ++index)
            WriteEncodedCharToStream((char)input[index], memoryStream, false);
        return memoryStream.ToArray();
    }

    public static string HtmlDecode(string s)
    {
        if (s == null)
            throw new ArgumentNullException(nameof(s));
        if (s.IndexOf('&') == -1)
            return s;
        var stringBuilder1 = new StringBuilder();
        var stringBuilder2 = new StringBuilder();
        var length = s.Length;
        var num1 = 0;
        var num2 = 0;
        var flag = false;
        for (var index = 0; index < length; ++index)
        {
            var c = s[index];
            if (num1 == 0)
            {
                if (c == '&')
                {
                    stringBuilder1.Append(c);
                    num1 = 1;
                }
                else
                    stringBuilder2.Append(c);
            }
            else if (c == '&')
            {
                num1 = 1;
                if (flag)
                {
                    stringBuilder1.Append(num2.ToString(CultureInfo.InvariantCulture));
                    flag = false;
                }

                stringBuilder2.Append(stringBuilder1.ToString());
                stringBuilder1.Length = 0;
                stringBuilder1.Append('&');
            }
            else
            {
                switch (num1)
                {
                    case 1:
                        if (c == ';')
                        {
                            num1 = 0;
                            stringBuilder2.Append(stringBuilder1.ToString());
                            stringBuilder2.Append(c);
                            stringBuilder1.Length = 0;
                            continue;
                        }

                        num2 = 0;
                        num1 = c == '#' ? 3 : 2;
                        stringBuilder1.Append(c);
                        continue;
                    case 2:
                        stringBuilder1.Append(c);
                        if (c == ';')
                        {
                            var str = stringBuilder1.ToString();
                            if (str.Length > 1 && SpecialHtmlCharacters.ContainsKey(str.Substring(1, str.Length - 2)))
                                str = SpecialHtmlCharacters[str.Substring(1, str.Length - 2)]?.ToString();
                            stringBuilder2.Append(str);
                            num1 = 0;
                            stringBuilder1.Length = 0;
                        }

                        continue;
                    case 3:
                        if (c == ';')
                        {
                            if (num2 > ushort.MaxValue)
                            {
                                stringBuilder2.Append("&#");
                                stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
                                stringBuilder2.Append(";");
                            }
                            else
                                stringBuilder2.Append((char)num2);

                            num1 = 0;
                            stringBuilder1.Length = 0;
                            flag = false;
                            continue;
                        }

                        if (char.IsDigit(c))
                        {
                            num2 = num2 * 10 + (c - 48);
                            flag = true;
                            continue;
                        }

                        num1 = 2;
                        if (flag)
                        {
                            stringBuilder1.Append(num2.ToString(CultureInfo.InvariantCulture));
                            flag = false;
                        }

                        stringBuilder1.Append(c);
                        continue;
                    default:
                        continue;
                }
            }
        }

        if (stringBuilder1.Length > 0)
            stringBuilder2.Append(stringBuilder1.ToString());
        else if (flag)
            stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
        return stringBuilder2.ToString();
    }

    public static string HtmlEncode(string input)
    {
        if (input == null)
            return null;
        var flag = false;
        for (var index = 0; index < input.Length; ++index)
        {
            var ch = input[index];
            if (ch == '&' || ch == '"' || ch == '<' || ch == '>' || ch > '\u009F')
            {
                flag = true;
                break;
            }
        }

        if (!flag)
            return input;
        var stringBuilder = new StringBuilder();
        var length = input.Length;
        for (var index = 0; index < length; ++index)
        {
            switch (input[index])
            {
                case '"':
                    stringBuilder.Append("&quot;");
                    break;
                case '&':
                    stringBuilder.Append("&amp;");
                    break;
                case '<':
                    stringBuilder.Append("&lt;");
                    break;
                case '>':
                    stringBuilder.Append("&gt;");
                    break;
                default:
                    if (input[index] > '\u009F')
                    {
                        stringBuilder.Append("&#");
                        stringBuilder.Append(((int)input[index]).ToString(CultureInfo.InvariantCulture));
                        stringBuilder.Append(";");
                        break;
                    }

                    stringBuilder.Append(input[index]);
                    break;
            }
        }

        return stringBuilder.ToString();
    }
}
