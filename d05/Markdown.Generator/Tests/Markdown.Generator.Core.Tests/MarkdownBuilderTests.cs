using System.Linq;
using Xunit;
using Markdown.Generator.Core.Markdown;
using Markdown.Generator.Core.Markdown.Elements;

namespace Markdown.Generator.Core.Tests
{
    public class MarkdownBuilderTests
    {
        [Fact]
        public void Given_MarkdownBuilder_When_CodeQuoteMethodCalled_Then_ContainsSingleCodeQuote()
        {
            var expected = new CodeQuote("code");
            var builder = new MarkdownBuilder();

            builder.CodeQuote("code");

            Assert.Single(builder.Elements);
            Assert.Equal(expected.Create(), builder.Elements.First().Create());
        }

        [Fact]
        public void Given_MarkdownBuilder_When_CodeMethodCalled_Then_ContainsSingleCodeQuote()
        {
            var expected = new Code("csharp", "code");
            var builder = new MarkdownBuilder();

            builder.Code("csharp", "code");

            Assert.Single(builder.Elements);
            Assert.Equal(expected.Create(), builder.Elements.First().Create());
        }

        [Fact]
        public void Given_MarkdownBuilder_When_LinkMethodCalled_Then_ContainsSingleCodeQuote()
        {
            var expected = new Link("title", "url");
            var builder = new MarkdownBuilder();

            builder.Link("title", "url");

            Assert.Single(builder.Elements);
            Assert.Equal(expected.Create(), builder.Elements.First().Create());
        }

        [Fact]
        public void Given_MarkdownBuilder_When_HeaderMethodCalled_Then_ContainsSingleCodeQuote()
        {
            var expected = new Header(2, "header");
            var builder = new MarkdownBuilder();

            builder.Header(2, "header");

            Assert.Single(builder.Elements);
            Assert.Equal(expected.Create(), builder.Elements.First().Create());
        }
    }
}
