using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Markdown.Generator.Core.Markdown;
using Markdown.Generator.Core.Markdown.Elements;

namespace Markdown.Generator.Core.Tests
{
    public class Sut
    {
        public void PublicMethod() { }
        private void PrivateMethod() { }

        public readonly int PublicField = 0;
        private readonly int _privateField = 0;
        
        public string PublicProperty => "prop";
        private string PrivateProperty => "prop";
    }

    public class MarkdownableTypeTests
    {
        [Fact]
        public void Given_MarkdownableType_When_GetMethodsMethodCalled_Then_ReturnPublicMethodsArray()
        {
            var mdType = new MarkdownableType(typeof(Sut), null);

            MethodInfo[] actual = mdType.GetMethods();

            Assert.False(actual.Where(meth => meth.IsPrivate).Any());
        }

        [Fact]
        public void Given_MarkdownableType_When_GetFieldsMethodCalled_Then_ReturnPublicFieldsArray()
        {
            var mdType = new MarkdownableType(typeof(Sut), null);

            FieldInfo[] actual = mdType.GetFields();

            Assert.False(actual.Where(field => field.IsPrivate).Any());
        }

        [Fact]
        public void Given_MarkdownableType_When_GetPropertiesMethodCalled_Then_ReturnPublicPropertiesArray()
        {
            var mdType = new MarkdownableType(typeof(Sut), null);

            PropertyInfo[] actual = mdType.GetProperties();

            Assert.False(actual.Where(prop => prop.PropertyType.IsNotPublic).Any());
        }
    }
}
