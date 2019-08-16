// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System.Collections.Generic;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="ICollection{T}"/> implementations.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the collection is <c>null</c> or empty.
        /// </summary>
        /// <param name="collection">The list to check.</param>
        /// <returns><c>true</c> if the passed <paramref name="collection"/> is either <c>null</c> or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection is null || collection.Count == 0;
        }
        
        /// <summary>
        /// Returns true if the <paramref name="collection"/>
        /// is not <c>null</c> and has at least 1 element in it.
        /// </summary>
        /// <param name="collection">The collection to check.</param>
        /// <returns><c><paramref name="collection"/> != null &amp;&amp; <paramref name="collection"/>.Length > 0</c></returns>
        public static bool NotNullNotEmpty<T>(this ICollection<T> collection)
        {
            return collection != null && collection.Count > 0;
        }
        
        /// <summary>
        /// Checks whether the two <see cref="ICollection{T}"/> are equal (have the same elements).<para> </para>
        /// The order of the elements is not important; e.g. {1,2,3} and {2,3,1} would return <c>true</c>.
        /// </summary>
        /// <typeparam name="T"><see cref="ICollection{T}"/> type parameter.</typeparam>
        /// <param name="a">Collection to compare.</param>
        /// <param name="b">Collection to compare.</param>
        /// <returns>Whether the two collections have the same elements.</returns>
        public static bool UnorderedEqual<T>(this ICollection<T> a, ICollection<T> b)
        {
            int count = a.Count;
            if (count != b.Count)
            {
                return false;
            }

            var dictionary = new Dictionary<T, int>(count);

            // Add each key's frequency from collection A to the Dictionary.
            foreach (T item in a)
            {
                if (dictionary.TryGetValue(item, out int i))
                {
                    dictionary[item] = i + 1;
                }
                else
                {
                    dictionary.Add(item, 1);
                }
            }

            // Add each key's frequency from collection B to the Dictionary.
            // Return early if we detect a mismatch.
            foreach (T item in b)
            {
                if (dictionary.TryGetValue(item, out int i))
                {
                    if (i == 0)
                    {
                        return false;
                    }
                    dictionary[item] = i - 1;
                }
                else
                {
                    // Not in dictionary.
                    return false;
                }
            }

            // Verify that all frequencies are zero.
            foreach (int v in dictionary.Values)
            {
                if (v != 0)
                {
                    return false;
                }
            }

            // At this point, we know that the collections are equal.
            return true;
        }
    }
}
