// Copyright (c) 2019, Raphael Beck. All rights reserved.
// Use of this source code is governed by the BSD 3-Clause license that can be found in the repository root directory's LICENSE file.

using Xunit;

namespace GlitchedPolygons.ExtensionMethods.UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsNullOrEmpty_Null_DoesNotThrow_ReturnsTrue()
        {
            string s = null;
            Assert.True(s.NullOrEmpty());
        }

        [Fact]
        public void BCrypt_ExtremelyBriefAndQuickSmokeTests_ShouldWork_BcryptNetPerSeAlreadyHasUnitTestsSoYeah()
        {
            string s1 = "idfjnidfbgKDFNGOLJDNOFOLkjfnikjdbijfKDNG...328728764ç%*ç&%&*äöüä¨==11¦°#§¬";
            string h1 = s1.BCrypt();
            string h1_2 = s1.BCrypt();
            Assert.NotEqual(h1, h1_2);
            Assert.True(h1.BCryptVerify(s1) && h1_2.BCryptVerify(s1));
            Assert.False(h1.BCryptVerify("WRONG STRING TO COMPARE - THIS SHOULD NOT RETURN TRUE"));
        }

        [Fact]
        public void BCryptEnhanced_ExtremelyBriefAndQuickSmokeTests_ShouldWork_BcryptNetPerSeAlreadyHasUnitTestsSoYeah()
        {
            string s1 = "idfjnidfbgKDKNENCONOBEOJBODLkjfnikjdbijfKDNG...4783468743%*ç&%&*äöüä¨=14¦°#§¬";
            string h1 = s1.BCrypt_Enhanced();
            string h1_2 = s1.BCrypt_Enhanced();
            Assert.NotEqual(h1, h1_2);
            Assert.True(h1.BCryptVerify_Enhanced(s1) && h1_2.BCryptVerify_Enhanced(s1));
            Assert.False(h1.BCryptVerify_Enhanced("WRONG STRING TO COMPARE - THIS SHOULD NOT RETURN TRUE"));
        }

        [Theory]
        [InlineData("email@example.com")]
        [InlineData("email-with-dashes@example.com")]
        [InlineData("email.with.dots-AND-UPPERCASE-CHARS_with_underscores@example.com")]
        [InlineData("email-with-special-chars-like_ñ_+-=?!$#@example.com")]
        public void IsValidEmail_ValidAddressesShouldReturnTrue(string email)
        {
            Assert.True(email.IsValidEmail());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("noat")]
        [InlineData("email@")]
        [InlineData("email@baddomain")]
        [InlineData("doubleat@@")]
        [InlineData("doubleat@@nodomain")]
        [InlineData("doubleat@@toomuch.com")]
        [InlineData("äüää-!$#  @example.com")]
        public void IsValidEmail_InvalidAddressesShouldReturnFalse(string email)
        {
            Assert.False(email.IsValidEmail());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email-address@example.com")]
        [InlineData(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~")]
        public void IsASCII_TrueAssertions(string str)
        {
            Assert.True(str.IsASCII());
        }

        [Theory]
        [InlineData("äüää")]
        [InlineData("¿¦èéà£")]
        [InlineData("°ç&%somenormalchars¨")]
        public void IsASCII_FalseAssertions(string str)
        {
            Assert.False(str.IsASCII());
        }
    }
}