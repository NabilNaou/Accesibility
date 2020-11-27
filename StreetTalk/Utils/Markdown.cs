using Microsoft.AspNetCore.Html;

namespace StreetTalk.Utils
{
    public static class Markdown
    {
        public static string Parse(string markdown, bool usePragmaLines = false, bool forceReload = false)
        {
            return string.IsNullOrEmpty(markdown) ? "" : Markdig.Markdown.ToHtml(markdown);
        }
        
        public static HtmlString ParseHtmlString(string markdown, bool usePragmaLines = false, bool forceReload = false)
        {
            return new HtmlString(Parse(markdown, usePragmaLines, forceReload));
        }
    }
}