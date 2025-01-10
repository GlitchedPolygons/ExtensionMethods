using System;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="Int64"/>.
    /// </summary>
    public static class Int64Extensions
    {
        private static readonly string[] SIZE_SUFFIX_STRINGS = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
        
        /// <summary>
        /// Converts a Unix timestamp (seconds since 1970-01-01T00:00:00Z) to a UTC <see cref="DateTime"/>.
        /// </summary>
        /// <param name="timestamp">The Unix timestamp to convert.</param>
        /// <returns>The converted <see cref="DateTime"/> in UTC.</returns>
        public static DateTime FromUnixTimeSeconds(this Int64 timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }
        
        /// <summary>
        /// Converts a Unix timestamp (milliseconds since 1970-01-01 00:00:00.000 UTC) to a <see cref="DateTime"/> (in UTC).
        /// </summary>
        /// <param name="timestamp">The Unix timestamp to convert.</param>
        /// <returns>The converted <see cref="DateTime"/> in UTC.</returns>
        public static DateTime FromUnixTimeMilliseconds(this Int64 timestamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
        }

        /// <summary>
        /// Gets the passed <paramref name="fileSizeInBytes"/> as a human readable file size string (e.g. "5 KB" or "20 MB").
        /// </summary>
        /// <param name="fileSizeInBytes">The bytes (for example a file whose size you want to convert into a human readable <see cref="String"/>).</param>
        /// <returns>A human readable file size <c>string</c> that represents the passed <paramref name="fileSizeInBytes"/>.</returns>
        public static string BytesToFileSizeString(this Int64 fileSizeInBytes)
        {
            Int64 byteCount = Math.Abs(fileSizeInBytes);
    
            if (byteCount == 0)
            {
                return "0" + SIZE_SUFFIX_STRINGS[0];
            }

            Int32 i = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
            
            double n = Math.Round(byteCount / Math.Pow(1024, i), 1);

            return (Math.Sign(byteCount) * n).ToString(System.Globalization.CultureInfo.InvariantCulture) + SIZE_SUFFIX_STRINGS[i];
        }
        
        /// <summary>
        /// Converts an excel column number such as 28 to its equivalent column address name (in the case of 28 for example that would be "AB"). 
        /// </summary>
        /// <param name="columnNumber">Column number to convert to Excel column address string format.</param>
        /// <returns>Column address/name, e.g. "A", "B", "C", "AB", etc...</returns>
        public static string ExcelColumnNumberToName(this Int64 columnNumber)
        {
            Int64 i = columnNumber;
        
            if (i <= 26)
            {
                return Convert.ToChar(i + 64).ToString();
            }

            Int64 div = i / 26;
            Int64 mod = i % 26;
        
            if (mod == 0)
            {
                mod = 26;
                --div;
            }

            return ExcelColumnNumberToName(div) + ExcelColumnNumberToName(mod);
        }
    }
}