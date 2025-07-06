// Copyright (c) 2025, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System.Collections.Generic;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="IReadOnlyCollection{T}"/> implementations.
    /// </summary>
    public static class ReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        /// <param name="collection">The list to check.</param>
        /// <returns><c>true</c> if the passed <paramref name="collection"/> is either <c>null</c> or empty; otherwise, <c>false</c>.</returns>
        public static bool NullOrEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            return collection is null || collection.Count == 0;
        }

        /// <summary>
        /// Returns true if the <paramref name="collection"/>
        /// is not <c>null</c> and has at least 1 element in it.
        /// </summary>
        /// <param name="collection">The collection to check.</param>
        /// <returns><c><paramref name="collection"/> != null &amp;&amp; <paramref name="collection"/>.Length > 0</c></returns>
        public static bool NotNullNotEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            return collection != null && collection.Count > 0;
        }

        /// <summary>
        /// Checks whether the two <see cref="IReadOnlyCollection{T}"/> are equal (have the same elements).<para> </para>
        /// The order of the elements is not important; e.g. {1,2,3} and {2,3,1} would return <c>true</c>.
        /// </summary>
        /// <typeparam name="T"><see cref="IReadOnlyCollection{T}"/> type parameter.</typeparam>
        /// <param name="a">Collection to compare.</param>
        /// <param name="b">Collection to compare.</param>
        /// <returns>Whether the two collections have the same elements.</returns>
        public static bool UnorderedEqual<T>(this IReadOnlyCollection<T> a, IReadOnlyCollection<T> b)
        {
            return CollectionUnorderedEqualityCheck.UnorderedEqual(a, b);
        }
    }
}