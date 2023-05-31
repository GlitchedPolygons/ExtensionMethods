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

        [Theory]
        [InlineData("äüää","1fbab778032879e67f2eca41a5e29bb8dca583a0")]
        [InlineData("¿¦èéà£","1ba5967b1365f9b5f013af34c9a670b606848677")]
        [InlineData("°ç&%somenormalchars¨","61a186a8d79af6144aa472005cc4c6a7800b6151")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","3b85c5ff465c50286acd4b9807f542f8367a92ed")]
        public void IsCorrectSHA1Bytes(string str, string hash)
        {
            Assert.True(str.UTF8GetBytes().SHA1() == hash.ToUpper());
            Assert.True(str.UTF8GetBytes().SHA1(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","bb71f56013462aaa01b8d542882873b48a8de842adaca2e27a8ca109ddba674b")]
        [InlineData("¿¦èéà£","444c85c6d5785b75b36d2268a1a6754cb53ee2deea2be1fb6d1fdba3aa7eda0b")]
        [InlineData("°ç&%somenormalchars¨","862794e23022dafba6f187af745424b8f032981936cc24b972cbbb757b38615e")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","02b768fac292cc2e20d538345a4d948a47540c7c2078c9dec3354ffcd8a9ba1c")]
        public void IsCorrectSHA256Bytes(string str, string hash)
        {
            Assert.True(str.UTF8GetBytes().SHA256() == hash.ToUpper());
            Assert.True(str.UTF8GetBytes().SHA256(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","14d8f8836d5071406192d56cb37abc4d92a0ab7834998d0f939973d2b433acf3e395c2e35e623515844bcb1fbe080d95")]
        [InlineData("¿¦èéà£","05c16c2e2035ec56150246085ffd609a2602d6efdfec5c9dd432a8a788c1189a6d74782a16c88e93ad673924ab530c66")]
        [InlineData("°ç&%somenormalchars¨38475hjfdnbg....,kj4iub","261326a4526d3f7315a7af31956b081d517c1a57571b7abb0c8d210fe47e9284b5d6775ddbe632f8eda20f0a6d8269b0")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","026505ffa1d4f15e374911d21b6d79e9dca407f6385da99be6090d266ef53199d41d6c8d899416c9806a5b1619182b33")]
        public void IsCorrectSHA384Bytes(string str, string hash)
        {
            Assert.True(str.UTF8GetBytes().SHA384() == hash.ToUpper());
            Assert.True(str.UTF8GetBytes().SHA384(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","b3349bb3e78987d0341e825692c194f14970cd67a789b731ba5ad62b1a8d166a800b4d35b0b94d10911f9efff8932aff65ddb98ee40062dd78dc83d6a400d975")]
        [InlineData("¿¦èéà£","8d7bf2e266e77d5f039373f059688498b7dd7316018b8529e54359a6a881ec61978d780c46b355fd505bea54eab2f697363f6b085cf61ff0b445ddf8eb4d9b14")]
        [InlineData("jhfbhbg°ç&%somenormalchars¨38475hjfdnbg....,kj4iub","a02ba41460ce507c418a0c2355a01e1e04c047e030f45ce6d4f5c1dad4fd07bc2caa81f309940d8a3bcfff28cdd8ef7dfc8430293fee3c87ce8638b635a9cfd0")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","e62a3b9d091b479464204512ac697ee9912c7a9615071f031c02e0833f1e12fb6da99f3f73d404b75e89c131885a98f73449c1a993a7c050ea198dd758afddeb")]
        public void IsCorrectSHA512Bytes(string str, string hash)
        {
            Assert.True(str.UTF8GetBytes().SHA512() == hash.ToUpper());
            Assert.True(str.UTF8GetBytes().SHA512(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","1fbab778032879e67f2eca41a5e29bb8dca583a0")]
        [InlineData("¿¦èéà£","1ba5967b1365f9b5f013af34c9a670b606848677")]
        [InlineData("°ç&%somenormalchars¨","61a186a8d79af6144aa472005cc4c6a7800b6151")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","3b85c5ff465c50286acd4b9807f542f8367a92ed")]
        public void IsCorrectSHA1(string str, string hash)
        {
            Assert.True(str.SHA1() == hash.ToUpper());
            Assert.True(str.SHA1(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","bb71f56013462aaa01b8d542882873b48a8de842adaca2e27a8ca109ddba674b")]
        [InlineData("¿¦èéà£","444c85c6d5785b75b36d2268a1a6754cb53ee2deea2be1fb6d1fdba3aa7eda0b")]
        [InlineData("°ç&%somenormalchars¨","862794e23022dafba6f187af745424b8f032981936cc24b972cbbb757b38615e")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","02b768fac292cc2e20d538345a4d948a47540c7c2078c9dec3354ffcd8a9ba1c")]
        public void IsCorrectSHA256(string str, string hash)
        {
            Assert.True(str.SHA256() == hash.ToUpper());
            Assert.True(str.SHA256(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","14d8f8836d5071406192d56cb37abc4d92a0ab7834998d0f939973d2b433acf3e395c2e35e623515844bcb1fbe080d95")]
        [InlineData("¿¦èéà£","05c16c2e2035ec56150246085ffd609a2602d6efdfec5c9dd432a8a788c1189a6d74782a16c88e93ad673924ab530c66")]
        [InlineData("°ç&%somenormalchars¨38475hjfdnbg....,kj4iub","261326a4526d3f7315a7af31956b081d517c1a57571b7abb0c8d210fe47e9284b5d6775ddbe632f8eda20f0a6d8269b0")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","026505ffa1d4f15e374911d21b6d79e9dca407f6385da99be6090d266ef53199d41d6c8d899416c9806a5b1619182b33")]
        public void IsCorrectSHA384(string str, string hash)
        {
            Assert.True(str.SHA384() == hash.ToUpper());
            Assert.True(str.SHA384(true) == hash.ToLower());
        }
        
        [Theory]
        [InlineData("äüää","b3349bb3e78987d0341e825692c194f14970cd67a789b731ba5ad62b1a8d166a800b4d35b0b94d10911f9efff8932aff65ddb98ee40062dd78dc83d6a400d975")]
        [InlineData("¿¦èéà£","8d7bf2e266e77d5f039373f059688498b7dd7316018b8529e54359a6a881ec61978d780c46b355fd505bea54eab2f697363f6b085cf61ff0b445ddf8eb4d9b14")]
        [InlineData("jhfbhbg°ç&%somenormalchars¨38475hjfdnbg....,kj4iub","a02ba41460ce507c418a0c2355a01e1e04c047e030f45ce6d4f5c1dad4fd07bc2caa81f309940d8a3bcfff28cdd8ef7dfc8430293fee3c87ce8638b635a9cfd0")]
        [InlineData("LOREM IPSUM dolor sick fuck amet something something ..","e62a3b9d091b479464204512ac697ee9912c7a9615071f031c02e0833f1e12fb6da99f3f73d404b75e89c131885a98f73449c1a993a7c050ea198dd758afddeb")]
        public void IsCorrectSHA512(string str, string hash)
        {
            Assert.True(str.SHA512() == hash.ToUpper());
            Assert.True(str.SHA512(true) == hash.ToLower());
        }
    }
}