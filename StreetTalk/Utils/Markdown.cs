using Markdig;
using Microsoft.AspNetCore.Html;

namespace StreetTalk.Utils
{
    public static class Markdown
    {
        private static string Parse(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().DisableHtml().Build();
            return string.IsNullOrEmpty(markdown) ? "" : Markdig.Markdown.ToHtml(markdown, pipeline);
        }
        
        public static HtmlString ParseHtmlString(string markdown)
        {
            return new HtmlString(Parse(markdown));
        }
    }
}