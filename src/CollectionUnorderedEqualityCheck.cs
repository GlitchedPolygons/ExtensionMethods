// Copyright (c) 2025, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System.Collections.Generic;

namespace GlitchedPolygons.ExtensionMethods
{
    internal static class CollectionUnorderedEqualityCheck
    {
        private static bool UnorderedEqual<T>(IEnumerable<T> a, IEnumerable<T> b, int countA, int countB)
        {
            if (countA != countB)
            {
                return false;
            }

            var dictionary = new Dictionary<T, int>(countA);

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
        
        internal static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
        {
            int countA = a.Count;
            int countB = b.Count;

            return UnorderedEqual(a, b, countA, countB);
        }
        
        internal static bool UnorderedEqual<T>(IReadOnlyCollection<T> a, IReadOnlyCollection<T> b)
        {
            int countA = a.Count;
            int countB = b.Count;
            
            return UnorderedEqual(a, b, countA, countB);
        }
    }
}