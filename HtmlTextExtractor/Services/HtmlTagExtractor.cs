using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HtmlTextExtractor.Services;

internal class HtmlTagExtractor
{
    /// <summary>
    /// This method extracts HTML tags from a given input string
    /// </summary>
    public static StringCollection ExtractHtmlTags(string tagName, string inputString, bool onlyOpeningTags)
    {
        var stringCollection = new StringCollection();
        string pattern;
        if (onlyOpeningTags)
            pattern = string.Format(CultureInfo.InvariantCulture,
                "<{0}(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>",
                Regex.Escape(tagName));
        else
            pattern = string.Format(CultureInfo.InvariantCulture,
                "<(/?){0}(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>",
                Regex.Escape(tagName));
        foreach (Match match in new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(
                     inputString))
            stringCollection.Add(match.Value);
        return stringCollection;
    }
}
