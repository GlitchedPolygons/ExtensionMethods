using System;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="Int64"/>.
    /// </summary>
    public static class Int64Extensions
    {
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
    }
}