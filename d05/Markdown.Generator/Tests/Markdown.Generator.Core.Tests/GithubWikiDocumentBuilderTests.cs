using System;
using System.Reflection;
using Xunit;
using Moq;
using Markdown.Generator.Core.Documents;
using Markdown.Generator.Core.Markdown;

namespace Markdown.Generator.Core.Tests
{
    public class GithubWikiDocumentBuilderTests
    {
        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_DllPathAndNamespaceMatchAsParameters_Then_LoadMethodCalledOnceOnGenerate()
        {
            var mock = new Mock<IMarkdownGenerator>();
            mock.Setup(md => md.Load(It.IsAny<string>(), It.IsAny<string>()));
            
            var builder = new GithubWikiDocumentBuilder<IMarkdownGenerator>(mock.Object);
            builder.Generate(It.IsAny<string>(), It.IsAny<string>(), Environment.CurrentDirectory);

            mock.Verify(gen => gen.Load(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_AssebliesAndNamespaceMatchAsParameters_Then_LoadMethodCalledOnceOnGenerate()
        {
            var mock = new Mock<IMarkdownGenerator>();
            mock.Setup(md => md.Load(It.IsAny<Assembly[]>(), It.IsAny<string>()));

            var builder = new GithubWikiDocumentBuilder<IMarkdownGenerator>(mock.Object);
            builder.Generate(It.IsAny<Assembly[]>(), It.IsAny<string>(), Environment.CurrentDirectory);

            mock.Verify(gen => gen.Load(It.IsAny<Assembly[]>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Given_GithubWikiDocumentBuilder_When_TypesAsParameters_Then_LoadMethodCalledOnceOnGenerate()
        {
            var mock = new Mock<IMarkdownGenerator>();
            mock.Setup(md => md.Load(It.IsAny<Type[]>()));

            var builder = new GithubWikiDocumentBuilder<IMarkdownGenerator>(mock.Object);
            builder.Generate(It.IsAny<Type[]>(), Environment.CurrentDirectory);

            mock.Verify(gen => gen.Load(It.IsAny<Type[]>()), Times.Once());
        }
    }
}
