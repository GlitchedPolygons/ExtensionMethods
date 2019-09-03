using System;
using System.Collections.Generic;
using Xunit;

namespace GlitchedPolygons.ExtensionMethods.UnitTests
{
    public class IEnumerableExtensionsTests
    {
        #region IEnumerable<T>

        [Fact]
        public void CompareCollections_Identical_ShouldReturn1()
        {
            string[] id1 = {"test1", "test2", "test3", "test4", "test5", "test6", "test7", "testDuplicate", "testDuplicate"};
            string[] id2 = {"test1", "test2", "test3", "test4", "test5", "test6", "test7", "testDuplicate", "testDuplicate", "testDuplicate"};

            float score = id1.Compare(id2);

            bool equal = Math.Abs(1.0f - score) < 0.001f;
            Assert.True(equal);
        }

        [Fact]
        public void CompareCollections_AlmostEqual_ShouldReturnAbove75percent()
        {
            string[] id1 = {"test1", "test2", "test3", "test4", "test5", "test6", "test7", "test8", "test9", "test10", "test11", "test12", "testDuplicate", "testDuplicate"};
            string[] id2 = {"test1", "test2", "test3", "test4", "TEST5", "TEST6", "TEST7", "test8", "test9", "test10", "test11", "test12", "testDuplicate", "testDuplicate", "testDuplicate"};

            float score = id1.Compare(id2);
            Assert.True(score > 0.75f);
        }

        [Fact]
        public void CompareCollections_HalfIdentical_ShouldReturn50percent()
        {
            ICollection<string> id1 = new[] {"test1", "test2", "test3", "test4", "test5", "test6"};
            ICollection<string> id2 = new[] {"WRONG1", "WRONG2", "WRONG3", "test4", "test5", "test6"};

            float score = id1.Compare(id2);

            bool equal = Math.Abs(0.5f - score) < 0.001f;
            Assert.True(equal);
        }

        [Fact]
        public void CompareCollections_AppendedManyRandomStrings_ShouldReturnLowScore()
        {
            List<string> id1 = new List<string> {"test1", "test2", "test3", "test4", "test5", "test6", "test7", "testDuplicate", "testDuplicate"};
            List<string> id2 = new List<string> {"test1", "test2", "test3", "test3"};

            for (int i = 0; i < 32; i++)
            {
                id2.Add(Guid.NewGuid().ToString());
            }

            float score = id1.Compare(id2);

            Assert.True(score < 0.1f);
        }

        [Fact]
        public void CompareCollections_ComparesEmptyCollection_ShouldReturn0()
        {
            IEnumerable<string> id1 = new List<string> {"test1", "test2", "test3", "test4", "test5", "test6", "test7", "testDuplicate", "testDuplicate"};
            IEnumerable<string> id2 = Array.Empty<string>();

            float score = id1.Compare(id2);

            Assert.True(score < 0.0001f);
        }

        #endregion

        #region IEnumerable<string>

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

        #endregion
    }
}