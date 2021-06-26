using System;
using Xunit;
using Markdown.Generator.Core.Markdown.Elements;

namespace Markdown.Generator.Core.Tests
{
    public class ElementsTests
    {
        [Fact]
        public void Given_Code_When_LanguageAndCodeAsParameter_Then_ReturnMarkdownCodeMarkup()
        {
            // arrange
            var expected = "```csharp\nsome code\n```\n";
            var code = new Code("csharp", "some code");
            
            // act
            var actual = code.Create();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
