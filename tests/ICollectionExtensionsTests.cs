using System.Collections.Generic;
using Xunit;

namespace GlitchedPolygons.ExtensionMethods.UnitTests
{
    public class ICollectionExtensionsTests
    {
        #region ICollection<T>

        [Fact]
        public void CompareCollections_Identical_ShouldSucceed()
        {
            ICollection<string> id1 = ["test2", "test1", "test3", "test4", "test5", "test6", "test7"];
            ICollection<string> id2 = ["test1", "test2", "test3", "test4", "test5", "test6", "test7"];

            Assert.True(id1.UnorderedEqual(id2));
        }

        [Fact]
        public void CompareCollections_NotIdentical_ShouldFail()
        {
            ICollection<string> id1 = ["test_WRONG", "test1", "test3", "test4", "test5", "test6", "test7"];
            ICollection<string> id2 = ["test1", "test2", "test3", "test4", "test5", "test6", "test7"];

            Assert.False(id1.UnorderedEqual(id2));
        }

        #endregion

        #region IReadOnlyCollection<string>

        [Fact]
        public void CompareReadOnlyCollections_Identical_ShouldSucceed()
        {
            IReadOnlyCollection<string> id1 = ["test2", "test1", "test3", "test4", "test5", "test6", "test7"];
            IReadOnlyCollection<string> id2 = ["test1", "test2", "test3", "test4", "test5", "test6", "test7"];

            Assert.True(id1.UnorderedEqualReadOnly(id2));
        }

        [Fact]
        public void CompareReadOnlyCollections_NotIdentical_ShouldFail()
        {
            IReadOnlyCollection<string> id1 = ["test_WRONG", "test1", "test3", "test4", "test5", "test6", "test7"];
            IReadOnlyCollection<string> id2 = ["test1", "test2", "test3", "test4", "test5", "test6", "test7"];

            Assert.False(id1.UnorderedEqualReadOnly(id2));
        }

        #endregion
    }
}