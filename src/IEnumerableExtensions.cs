using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// <c>IEnumerable&lt;T&gt; extension methods.</c>
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// The initial capacity of the internal <see cref="StringBuilder"/>.<para> </para>
        /// Try to give a rough estimate of your output string's length here.
        /// </summary>
        public static int StringBuilderInitialCap = 64;

        #region IEnumerable<T>

        /// <summary>
        /// Compares two collections and returns a score.<para> </para>
        /// Ideally, you'd set a carefully selected threshold of equality (such as 0.75f), but NOT 100%.<para> </para>
        /// The resulting equality score is a <c>float</c> value between [0;1] where <c>0</c> is completely different and <c>1</c> entirely identical.<para> </para>
        /// Duplicate values are stripped from both collections before comparison.
        /// </summary>
        /// <param name="collection1">Collection 1</param>
        /// <param name="collection2">Collection 2</param>
        /// /// <typeparam name="T"><see cref="ICollection{T}"/> type parameter.</typeparam>
        /// <returns>The resulting equality score: a value between [0;1] where 0 is completely different and 1 entirely identical.</returns>
        public static float Compare<T>(this IEnumerable<T> collection1, IEnumerable<T> collection2)
        {
            IList<T> l1 = collection1.Distinct().OrderBy(e => e).ToList();
            IList<T> l2 = collection2.Distinct().OrderBy(e => e).ToList();

            int c1 = l1.Count;
            int c2 = l2.Count;

            if (c1 != c2)
            {
                IList<T> smaller = c1 < c2 ? l1 : l2;
                IList<T> bigger = c1 > c2 ? l1 : l2;

                do smaller.Add(default(T));
                while (smaller.Count != bigger.Count);

                l1 = smaller;
                l2 = bigger;
                c1 = l1.Count;
                c2 = l2.Count;

                if (c1 != c2) throw new SystemException();
            }

            return (float) l1.Intersect(l2).Count() / c1;
        }

        #endregion

        #region IEnumerable<string>

        /// <summary>
        /// Converts a collection of <c>string</c>s to a single, comma-separated <c>string</c>.
        /// </summary>
        /// <param name="e">The collection of <c>string</c>s to convert.</param>
        /// <returns>The comma-separated <c>string</c> containing the input <c>string</c>s (e.g. "element1,element2,element3").</returns>
        public static string ToCommaSeparatedString(this IEnumerable<string> e)
        {
            return ToCustomCharSeparatedString(e, ',');
        }

        /// <summary>
        /// Converts a collection of <c>string</c>s to a single, custom-character-separated <c>string</c>.<para> </para>
        /// Make ABSOLUTELY sure that none of the <c>string</c>s in the collection contains the <paramref name="separatorChar"/>!!<para> </para>
        /// It would mess with the reverse method (which is <c>string.Split(<paramref name="separatorChar"/>)</c>)
        /// </summary>
        /// <param name="strings">The collection of <c>string</c>s to convert.</param>
        /// <param name="separatorChar">The <c>char</c> that will separate one entry from the other in the output <c>string</c>. Could be '|', '+' or something like that, just ensure that it is unique (no string inside the collection should contain the separator char!!!).</param>
        /// <returns>The flattened <c>string</c>, where each <c>string</c> is separated by <paramref name="separatorChar"/> from one another.</returns>
        /// <exception cref="ArgumentException">Thrown when one or more <c>string</c>s inside the <paramref name="strings"/> input collection contains the <paramref name="separatorChar"/> character.</exception>
        public static string ToCustomCharSeparatedString(this IEnumerable<string> strings, char separatorChar)
        {
            var sb = new StringBuilder(StringBuilderInitialCap);

            foreach (string str in strings)
            {
                if (str.IndexOf(separatorChar) != -1)
                {
                    throw new ArgumentException($"{nameof(IEnumerableExtensions)}::{nameof(ToCustomCharSeparatedString)}: One or more strings contains the separator char (a '{separatorChar}' in this case). This would mess with the reverse method (string.Split(char)). Problematic string: {str}");
                }

                sb.Append(str).Append(separatorChar);
            }

            return sb.ToString().TrimEnd(separatorChar);
        }

        #endregion
    }
}