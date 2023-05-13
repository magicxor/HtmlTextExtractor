using System.Text.RegularExpressions;

namespace HtmlTextExtractor.Services;

internal static class RegularExpressions
{
    public static readonly Regex RegexHrefSrcText = new("href[\\s]*=[\\s]*(?<srcText>(['\"][^'\"]+['\"])|(\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static readonly Regex RegexAltText = new("alt[\\s]*=[\\s]*(?<altText>(['\"][^'\"]+['\"])|(\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static readonly Regex RegexSrcText = new("src[\\s]*=[\\s]*[']{0,1}[\"]{0,1}(?<srcText>[\\S]*)[']{0,1}[\"]{0,1}[\\s]{1,}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static readonly Regex RegexBody = new("<(/?)body(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static readonly Regex RegexPre = new("<pre[^\\>]*>((?!\\<pre\\>.*\\</pre\\>).*?)</pre>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
    public static readonly Regex RegexWhiteSpace = new("(\\s)+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static readonly Regex RegexStyle = new("<style[^\\>]*>((?!\\<style\\>.*\\</style\\>).*?)</style>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
    public static readonly Regex RegexScript = new("<script[^\\>]*>((?!\\<script\\>.*\\</script\\>).*?)</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
    public static readonly Regex RegexA = new("<a(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>(?<tagContent>[^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
}
