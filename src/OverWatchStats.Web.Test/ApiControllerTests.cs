using Microsoft.AspNetCore.Mvc;
using Moq;
using OverwatchStats.Web.Api;
using OverwatchStats.Web.OverwatchApi.Dto;
using Xunit;

namespace OverwatchStats.Web.Test
{
    // There are not that many tests here.
    // Decided to drop typical AAA approach.
    // And move to 'shared' Arranged data
    public class ApiControllerTests
    {
        private readonly ApiController _sut;
        private const string CorrectBattleNetTag = "one";

        public ApiControllerTests()
        {
            var dummyResponse = new ProfileStats
            {
                MedalsGold = 10,
                MedalsSilver = 5,
                MedalsBronze = 1
            };

            var profileStatsProviderMock = new Mock<IProfileStatsProviderAsync>();
            profileStatsProviderMock.Setup(r => r.GetProfileStats(CorrectBattleNetTag))
                                  .ReturnsAsync(dummyResponse);

            _sut = new ApiController(profileStatsProviderMock.Object);
        }

        [Fact]
        public async void GetProfileStats_Returns200_ForCorrectRequest()
        {
            var actual = await _sut.GetProfileStats(CorrectBattleNetTag);

            Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(200, actual.StatusCode);
            Assert.IsType<ProfileStats>(actual.Value);

            var actualResponse = actual.Value as ProfileStats;
            Assert.NotNull(actualResponse);
            Assert.Equal(5, actualResponse.MedalsSilver);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Returns_400_ForNullEmptyBattleTag(string nullEmptyBattleTag)
        {
            var actual = await _sut.GetProfileStats(nullEmptyBattleTag);

            Assert.IsType<BadRequestObjectResult>(actual);
            Assert.Equal(400, actual.StatusCode); ;
        }
    }
}
