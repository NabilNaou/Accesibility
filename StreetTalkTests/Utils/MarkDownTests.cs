using StreetTalk.Utils;
using Xunit;

namespace StreetTalkTests.Utils
{
    public class MarkDownTests : BaseTest
    {

        [Fact]
        public void ParseHtmlString()
        {
            var result = Markdown.ParseHtmlString("# Test");
            Assert.Equal("<h1>Test</h1>\n", result.Value);
        }
        
    }
}