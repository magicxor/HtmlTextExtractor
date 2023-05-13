namespace HtmlTextExtractor;

public static class HtmlTextService
{
    public static string MakePlainBodyFromHtmlBody(string text, HtmlToPlainConvertOptions htmlToPlainOptions = HtmlToPlainConvertOptions.None)
    {
        return HtmlToPlainTextConverter.ConvertHtmlToPlainText(text, htmlToPlainOptions, false);
    }
}
