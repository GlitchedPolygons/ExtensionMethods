using System;
using System.Collections.Generic;

using Xunit;

namespace GlitchedPolygons.ExtensionMethods.UnitTests
{
    public class IEnumerableStringExtensionsTests
    {
        [Theory]
        [InlineData("test1", "test2", "te,st3_uh_oh...")]
        [InlineData("test1", "test2", "test3", "test4", "test5,")]
        public void ToCommaSeparatedList_OneOrMoreStringsContainsComma_ThrowsException(params string[] strings)
        {
            Assert.Throws<ArgumentException>(strings.ToCommaSeparatedString);
        }

        [Theory]
        [InlineData("test1", "test2", "test3", "test4", "test5", "test6")]
        [InlineData("test1", "SPECIAL_charächtërsss*#°§¬   !!", "test3", "test4", "test5", "test6", "  ", ".", " ", "@@\\\\ \\ \t\n")]
        public void ToCommaSeparatedList_ShouldReturnCorrectly(params string[] strings)
        {
            string o = strings.ToCommaSeparatedString();
            Assert.True(o.NotNullNotEmpty());
            Assert.Equal(strings, o.Split(','));
        }

        [Fact]
        public void ToCommaSeparatedList_EmptyStringsArray_ShouldReturnEmptyString()
        {
            Assert.Empty(Array.Empty<string>().ToCommaSeparatedString());
        }
    }
}