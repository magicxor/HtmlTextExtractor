namespace HtmlTextExtractor;

/// <summary>
/// Defines the available flags which affect how HTML body of a message is converted into plain text.
/// <seealso cref="P:MailBee.Mime.MailMessage.Parser" />
/// <seealso cref="P:MailBee.Mime.MessageParserConfig.HtmlToPlainOptions" />
/// <seealso cref="P:MailBee.Mime.MessageParserConfig.HtmlToPlainMode" />
/// </summary>
/// <remarks>
/// You can set any combination of these options via <see cref="P:MailBee.Mime.MessageParserConfig.HtmlToPlainOptions">MailMessage.Parser.HtmlToPlainOptions</see> property.
/// <para>By default (<see cref="F:MailBee.Mime.HtmlToPlainConvertOptions.None" /> value), all HTML tags are removed and line terminators are inserted instead of HTML's line terminators such as &lt;br&gt;, &lt;p&gt;, etc.</para>
/// <note>URI is a synonym of an URL.</note>
/// </remarks>
[Flags]
public enum HtmlToPlainConvertOptions
{
    /// <summary>
    /// No extra processing.
    /// </summary>
    None = 0,
    /// <summary>
    /// Alternative text contained in the <i>ALT</i> attribute of the <i>&lt;IMG&gt;</i> tag will be put into plain-text.
    /// </summary>
    AddImgAltText = 1,
    /// <summary>
    /// If alternative text was not set in the <i>&lt;IMG&gt;</i> tag, the <i>image</i> string will be put into
    /// plain text.
    /// </summary>
    WriteImageIfNoAlt = 2,
    /// <summary>
    /// For each image, its URI will be placed into plain text.
    /// </summary>
    AddUriForImg = 4,
    /// <summary>
    /// The URI contained in the &lt;a href=""&gt; tag will be put into plain-text after the content of this tag.
    /// For instance, if HTML was <i>&lt;a href="http://www.afterlogic.com"&gt;AfterLogic Corporation&lt;/a&gt;</i>, the plain-text version
    /// will be <i>AfterLogic Corporation &lt;http://www.afterlogic.com&gt;</i>.
    /// </summary>
    AddUriForAHRef = 8,
}