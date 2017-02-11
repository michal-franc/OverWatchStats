using System;
using Moq;
using OverwatchStats.Web.OverwatchApi.Dto;
using OverwatchStats.Web.OverwatchApi;
using Xunit;

namespace OverwatchStats.Web.Test
{
    public class OverwatchApiClientAsyncTests
    {
        private readonly OverwatchApiClientAsync _sut;

        public OverwatchApiClientAsyncTests()
        {
            var mockedClient = new Mock<IOverwatchApiClientAsync>();
            mockedClient.Setup(c => c.GetProfileStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(new ProfileStats());

            _sut = new OverwatchApiClientAsync(mockedClient.Object);
        }

        [Fact]
        public void ctor_IfApiClientNull_ThrowException() 
            => Assert.Throws<ArgumentNullException>(() => new OverwatchApiClientAsync(null));

        [Fact]
        public async void GetProfileStats_ThrowArgumentNullException_IfBattleTagNull()
            => await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetProfileStats(null));

        [Theory]
        [InlineData("^^")]
        public async void GetProfileStats_ThrowArgumentException_IfBattleTagInvalid(string invalidOutCode)
            => await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetProfileStats(invalidOutCode));

        [Fact]
        public async void GetProfileStats_ShouldReturnProfileStats()
        {
            const string correctBattleTag = "Lamzorjek#5462";

            var actual = await _sut.GetProfileStats(correctBattleTag);

            Assert.NotNull(actual);
            Assert.IsType<ProfileStats>(actual);
        }
    }
}
