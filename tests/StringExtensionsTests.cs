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
            Assert.NotEqual(h1,h1_2);
            Assert.True(h1.BCryptVerify(s1) && h1_2.BCryptVerify(s1));
            Assert.False(h1.BCryptVerify("WRONG STRING TO COMPARE - THIS SHOULD NOT RETURN TRUE"));
        }
        
        [Fact]
        public void BCryptEnhanced_ExtremelyBriefAndQuickSmokeTests_ShouldWork_BcryptNetPerSeAlreadyHasUnitTestsSoYeah()
        {
            string s1 = "idfjnidfbgKDKNENCONOBEOJBODLkjfnikjdbijfKDNG...4783468743%*ç&%&*äöüä¨=14¦°#§¬";
            string h1 = s1.BCrypt_Enhanced();
            string h1_2 = s1.BCrypt_Enhanced();
            Assert.NotEqual(h1,h1_2);
            Assert.True(h1.BCryptVerify_Enhanced(s1) && h1_2.BCryptVerify_Enhanced(s1));
            Assert.False(h1.BCryptVerify_Enhanced("WRONG STRING TO COMPARE - THIS SHOULD NOT RETURN TRUE"));
        }
    }
}
