using System;
using Xunit;
using Markdown.Generator.Core.Markdown.Elements;

namespace Markdown.Generator.Core.Tests
{
    public class ElementsTests
    {
        private static readonly string nl = Environment.NewLine;    //  windows compability

        [Fact]
        public void Given_Code_When_LanguageAndCodeAsParameter_Then_ReturnMarkdownCodeMarkup()
        {
            // arrange
            var expected = $"```csharp{nl}some code{nl}```{nl}";
            var code = new Code("csharp", "some code");
            
            // act
            var actual = code.Create();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_CodeQuote_When_CodeAsParameter_Then_ReturnMarkdownCodeQuoteMarkup()
        {
            var expected = "`code`";
            var codeQuote = new CodeQuote("code");

            var actual = codeQuote.Create();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_Header_When_LevelAndTextAsParameter_Then_ReturnMarkdownHeaderMarkup()
        {
            var expected = $"## header{nl}";
            var header = new Header(2, "header");

            var actual = header.Create();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Given_List_When_TextAsParameter_Then_ReturnMarkdownListMarkup()
        {
            var expected = $"- list{nl}";
            var list = new List("list");

            var actual = list.Create();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_Link_TextAndUrlAsParameter_Then_ReturnMarkdownLinkMarkup()
        {
            var expected = $"[title](url)";
            var link = new Link("title", "url");

            var actual = link.Create();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Given_Image_AltTextAndImageUrlAsParameter_Then_ReturnMarkdownImageMarkup()
        {
            var expected = $"![title](url)";
            var image = new Image("title", "url");

            var actual = image.Create();

            Assert.Equal(expected, actual);
        }
    }
}
