using System;
using System.Text;
using System.Collections.Generic;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// <c>IEnumerable&lt;string&gt; extension methods.</c>
    /// </summary>
    public static class IEnumerableStringExtensions
    {
        /// <summary>
        /// The initial capacity of the internal <see cref="StringBuilder"/>.<para> </para>
        /// Try to give a rough estimate of your output string's length here.
        /// </summary>
        public static int StringBuilderInitialCap = 64;

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
                    throw new ArgumentException($"{nameof(IEnumerableStringExtensions)}::{nameof(ToCustomCharSeparatedString)}: One or more strings contains the separator char (a '{separatorChar}' in this case). This would mess with the reverse method (string.Split(char)). Problematic string: {str}");
                }
                sb.Append(str).Append(separatorChar);
            }

            return sb.ToString().TrimEnd(separatorChar);
        }
    }
}