// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace GlitchedPolygons.ExtensionMethods
{
    /// <summary>
    /// Extension methods for all <c>string</c>s.
    /// </summary>
    public static class StringExtensions
    {
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
        /// Hashes the <c>string</c> using the BCrypt algorithm.<para> </para>
        /// Only use <see cref="BCryptVerify"/> for verifying/comparing!
        /// </summary>
        /// <param name="text">The <c>string</c> to BCrypt.</param>
        /// <param name="logRounds">The log2 of the number of rounds of hashing to apply. The work factor increases as (2 ** logRounds). The higher, the safer, the slower.</param>
        /// <returns>The hashed <c>string</c>.</returns>
        public static string BCrypt(this string text, int logRounds = 12)
        {
            return global::BCrypt.Net.BCrypt.HashPassword(text, logRounds);
        }

        /// <summary>
        /// Hashes the <c>string</c> using SHA384 + BCrypt algorithm.<para> </para>
        /// Only use <see cref="BCryptVerify_Enhanced"/> for verifying/comparing!
        /// </summary>
        /// <param name="text">The <c>string</c> to BCrypt.</param>
        /// <param name="logRounds">The log2 of the number of rounds of hashing to apply. The work factor increases as (2 ** logRounds). The higher, the safer, the slower.</param>
        /// <returns>The hashed <c>string</c>.</returns>
        public static string BCrypt_Enhanced(this string text, int logRounds = 12)
        {
            return global::BCrypt.Net.BCrypt.EnhancedHashPassword(text, logRounds);
        }

        /// <summary>
        /// Verifies a BCrypted <c>string</c> (obtained using <see cref="BCrypt"/>) against its plaintext counterpart.
        /// </summary>
        /// <param name="bcryptedString">The BCrypt hash of the <c>string</c> to verify.</param>
        /// <param name="plaintextToCompare">The plaintext <c>string</c> that was hashed.</param>
        /// <returns>Whether the plaintext string + hash could be verified.</returns>
        public static bool BCryptVerify(this string bcryptedString, string plaintextToCompare)
        {
            return global::BCrypt.Net.BCrypt.Verify(plaintextToCompare, bcryptedString);
        }

        /// <summary>
        /// Verifies a BCrypted <c>string</c> that was obtained using <see cref="BCrypt_Enhanced"/>.
        /// </summary>
        /// <param name="bcryptedString">The BCrypt hash of the <c>string</c> to verify.</param>
        /// <param name="plaintextToCompare">The plaintext <c>string</c> that was hashed.</param>
        /// <returns>Whether the plaintext string + hash could be verified.</returns>
        public static bool BCryptVerify_Enhanced(this string bcryptedString, string plaintextToCompare)
        {
            return global::BCrypt.Net.BCrypt.EnhancedVerify(plaintextToCompare, bcryptedString);
        }

        /// <summary>
        /// Computes the MD5 hash of a <c>string</c>.
        /// </summary>
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

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString(toLowercase ? "x2" : "X2"));
            }

            return stringBuilder.ToString();
        }
    }
}