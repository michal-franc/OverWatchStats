using OverwatchStats.Web.OverwatchApi.Validators;
using Xunit;

namespace OverwatchStats.Web.Test
{
    public class BattleTagValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData("                                    ")]
        [InlineData("@!#!@#!@#!")]
        [InlineData("~~!!@")]
        [InlineData("se18@")]
        [InlineData("se19@")]
        [InlineData("se19_")]
        [InlineData("   se19")]
        [InlineData("SE19           ")]
        [InlineData("               SE19           ")]
        [InlineData("SE19 ")]
        [InlineData("漢字")]
        [InlineData("Lamzorjek2342")]
        [InlineData("Lamzorjek##2342")]
        public void InvalidOutCode(string outCode)
            => Assert.True(BattleTagValidator.IsInvalid(outCode));

        [Theory]
        [InlineData("Lamzorjek#2342")]
        public void ValidOutCode(string outCode)
            => Assert.False(BattleTagValidator.IsInvalid(outCode));
    }
}
