﻿// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for all <c>string</c>s.
    /// </summary>
    public static class StringExtensions
    {
        private const string EMAIL_REGEX_PATTERN =
            @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

        /// <summary>
        /// Opens the <c>string</c> URL in the browser.
        /// </summary>
        /// <param name="url">The URL <c>string</c> to open.</param>
        public static void OpenUrlInBrowser(this string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Checks if a given <c>string</c> is a valid email address or not.
        /// </summary>
        /// <param name="str">The email address to validate.</param>
        /// <returns>Whether the given email address <c>string</c> is valid or not.</returns>
        public static bool IsValidEmail(this string str)
        {
            return str.NotNullNotEmpty() && Regex.IsMatch(str, EMAIL_REGEX_PATTERN, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Checks whether a given <c>string</c> only contains ASCII characters or not.<para> </para>
        /// <c>null</c> or empty <c>string</c>s return <c>true</c>!.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns>Whether the checked <c>string</c> only contained ASCII chars or not. If the <c>string</c> was empty or <c>null</c>, <c>true</c> is returned.</returns>
        public static bool IsASCII(this string str)
        {
            if (str.NullOrEmpty())
            {
                return true;
            }

            bool ascii = true;

            foreach (char c in str)
            {
                if (c >= 128)
                {
                    ascii = false;
                }
            }

            return ascii;
        }

        /// <summary>
        /// Returns <c>true</c> when the passed <c>string</c> is not <c>null</c> or empty; <c>false</c> otherwise.
        /// </summary>
        /// <param name="str">The <c>string</c> to check.</param>
        /// <returns><c>!string.IsNullOrEmpty(str)</c></returns>
        public static bool NotNullNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Returns whether the passed string is <c>null</c> or empty.
        /// </summary>
        /// <param name="str">The <c>string</c> to check.</param>
        /// <returns><c>string.IsNullOrEmpty(str)</c></returns>
        public static bool NullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Encodes a <c>string</c> to bytes using <c>Encoding.UTF8</c>.
        /// </summary>
        /// <param name="text">The text to encode.</param>
        /// <returns>The encoded <c>byte[]</c> array.</returns>
        public static byte[] UTF8GetBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// Computes the MD5 hash of a <c>string</c>.
        /// </summary>
        /// <remarks>
        /// Do not use MD5 for anything security-related! Do NOT hash passwords using this!
        /// </remarks>
        /// <param name="text">The text to hash.</param>
        /// <param name="toLowercase">Should the output hash be lowercased?</param>
        /// <returns>MD5 hash of the input string.</returns>
        public static string MD5(this string text, bool toLowercase = false)
        {
            using (HashAlgorithm md5 = System.Security.Cryptography.MD5.Create())
            {
                return HashString(text, toLowercase, md5);
            }
        }

        /// <summary>
        /// Computes the SHA1 of a <c>string</c>.
        /// </summary>
        /// <remarks>
        /// Do not use MD5 for anything security-related! Do NOT hash passwords using this!
        /// </remarks>
        /// <param name="text">The text to hash.</param>
        /// <param name="toLowercase">Should the output hash <c>string</c> be lowercased?.</param>
        /// <returns>SHA1 of the input string.</returns>
        public static string SHA1(this string text, bool toLowercase = false)
        {
            using (HashAlgorithm algo = System.Security.Cryptography.SHA1.Create())
            {
                return HashString(text, toLowercase, algo);
            }
        }

        /// <summary>
        /// Computes the SHA256 of a <c>string</c>.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <param name="toLowercase">Should the output hash <c>string</c> be lowercased?.</param>
        /// <returns>SHA256 of the input string.</returns>
        public static string SHA256(this string text, bool toLowercase = false)
        {
            using (HashAlgorithm algo = System.Security.Cryptography.SHA256.Create())
            {
                return HashString(text, toLowercase, algo);
            }
        }

        /// <summary>
        /// Computes the SHA384 of a <c>string</c>.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <param name="toLowercase">Should the output hash <c>string</c> be lowercased?.</param>
        /// <returns>SHA384 of the input string.</returns>
        public static string SHA384(this string text, bool toLowercase = false)
        {
            using (HashAlgorithm algo = System.Security.Cryptography.SHA384.Create())
            {
                return HashString(text, toLowercase, algo);
            }
        }

        /// <summary>
        /// Computes the SHA512 of a <c>string</c>.
        /// </summary>
        /// <param name="text">The text to hash.</param>
        /// <param name="toLowercase">Should the output hash <c>string</c> be lowercased?.</param>
        /// <returns>SHA512 of the input string.</returns>
        public static string SHA512(this string text, bool toLowercase = false)
        {
            using (HashAlgorithm algo = System.Security.Cryptography.SHA512.Create())
            {
                return HashString(text, toLowercase, algo);
            }
        }

        private static string HashString(string str, bool toLowercase, HashAlgorithm algo)
        {
            var stringBuilder = new StringBuilder(128);
            byte[] hash = algo.ComputeHash(str.UTF8GetBytes());
            string f = toLowercase ? "x2" : "X2";
            for (long i = 0; i < hash.LongLength; ++i)
            {
                stringBuilder.Append(hash[i].ToString(f));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a base64-encoded <see cref="String"/> to bytes, appending '=' padding characters to it if needed.
        /// </summary>
        /// <param name="b64">The string to convert.</param>
        /// <returns><c>Convert.FromBase64String(b64)</c></returns>
        public static byte[] ToBytesFromBase64(this string b64)
        {
            while (b64.Length % 4 != 0)
            {
                b64 += '=';
            }

            return Convert.FromBase64String(b64);
        }

        /// <summary>
        /// Converts a base64-URL-encoded <see cref="String"/> to bytes, appending '=' padding characters to it if needed.
        /// </summary>
        /// <param name="b64url">The string to convert.</param>
        /// <returns><c>Convert.FromBase64String(b64)</c></returns>
        public static byte[] ToBytesFromBase64Url(this string b64url)
        {
            return ToBytesFromBase64
            (
                b64url
                    .Replace('-', '+')
                    .Replace('_', '/')
            );
        }

        /// <summary>
        /// Converts a <see cref="String"/> to a Base64-encoded string of its UTF-8 bytes.
        /// </summary>
        /// <param name="str"><see cref="String"/> to encode.</param>
        /// <returns><see cref="String"/></returns>
        public static string ToBase64String(this string str)
        {
            return str.UTF8GetBytes().ToBase64String();
        }

        /// <summary>
        /// Converts a <see cref="String"/> to a Base64-URL-encoded string of its UTF-8 bytes.
        /// </summary>
        /// <param name="str"><see cref="String"/> to encode.</param>
        /// <returns><see cref="String"/></returns>
        public static string ToBase64UrlString(this string str)
        {
            return str.UTF8GetBytes()
                .ToBase64String()
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }

        /// <summary>
        /// Base64-decodes a <see cref="String"/> into a UTF-8 <see cref="String"/>.
        /// </summary>
        /// <param name="str"><see cref="String"/> to decode.</param>
        /// <returns><see cref="String"/></returns>
        public static string FromBase64String(this string str)
        {
            return str.ToBytesFromBase64().UTF8GetString();
        }

        /// <summary>
        /// Base64-URL-decodes a <see cref="String"/> into a UTF-8 <see cref="String"/>.
        /// </summary>
        /// <param name="str"><see cref="String"/> to decode.</param>
        /// <returns><see cref="String"/></returns>
        public static string FromBase64UrlString(this string str)
        {
            return str
                .Replace('-', '+')
                .Replace('_', '/')
                .ToBytesFromBase64()
                .UTF8GetString();
        }
    }
}