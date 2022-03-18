// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System;
using System.Text;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <c>byte[]</c> arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        private static readonly string[] SIZE_SUFFIX_STRINGS = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };

        /// <summary>
        /// Gets the <c>byte[]</c> array's human readable file size string (e.g. "5 KB" or "20 MB").
        /// </summary>
        /// <param name="bytes">The bytes (for example a file whose size you want to retrieve in a human readable way).</param>
        /// <returns>A human readable file size <c>string</c> that represents the passed <c>byte[]</c> array's length (<c>string.Empty</c> if the array was <c>null</c>).</returns>
        public static string GetFileSizeString(this byte[] bytes)
        {
            if (bytes is null)
            {
                return string.Empty;
            }

            long byteCount = Math.Abs(bytes.LongLength);
            if (byteCount == 0)
            {
                return "0" + SIZE_SUFFIX_STRINGS[0];
            }

            int i = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
            double n = Math.Round(byteCount / Math.Pow(1024, i), 1);
            
            return (Math.Sign(byteCount) * n).ToString(System.Globalization.CultureInfo.InvariantCulture) + SIZE_SUFFIX_STRINGS[i];
        }
        
        /// <summary>
        /// Returns <c>Encoding.UTF8.GetString(byte[])</c>.
        /// </summary>
        /// <param name="bytes">The <c>byte[]</c> array to convert.</param>
        /// <returns>Returns <c>Encoding.UTF8.GetString(byte[])</c></returns>
        public static string UTF8GetString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        
        /// <summary>
        /// Converts a <c>byte[]</c> array to a Base64-encoded <see cref="String"/> with optional removal of the padding '=' characters.
        /// </summary>
        /// <param name="bytes">The bytes to encode.</param>
        /// <param name="omitPaddingChars">Should the <c>=</c> padding characters be omitted?</param>
        /// <returns>The Base64-encoded string.</returns>
        public static string ToBase64String(this byte[] bytes, bool omitPaddingChars = false)
        {
            string output = Convert.ToBase64String(bytes);
            return omitPaddingChars ? output.TrimEnd('=') : output;
        }
    }
}
