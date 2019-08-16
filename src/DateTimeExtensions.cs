// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System;
using System.Globalization;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts <see cref="DateTime"/> to a RFC3339 <c>string</c>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to stringify.</param>
        /// <returns>The <see cref="DateTime"/> as a RFC3339 <c>string</c></returns>
        public static string ToRfc3339String(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", DateTimeFormatInfo.InvariantInfo);
        }
    }
}