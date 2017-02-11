using System;
using Xunit;

namespace OverWatchStats.Web.E2ETests
{
    public class GetProfileStatsTests
    {
        private readonly Uri _webDriverUri = new Uri("http://localhost:9515");

        [Fact]
        public void ThereIsNoProfileFoundMessage_WhenBattleTag_NonExisting()
        {
            const string nonExistingBattleTag = "Lamzorjek#1312312312312";

            using (var homePage = new HomePageObject(_webDriverUri))
            {
                homePage.PerformSearchFor(nonExistingBattleTag);
                Assert.True(homePage.ThereIsNoProfileFoundMessage());
            }
        }

        [Fact]
        public void ProfileStatsAreVisible_ForCorrectOutCode()
        {
            const string correctBattleTag = "Lamzorjek#2342";

            using (var homePage = new HomePageObject(_webDriverUri))
            {
                homePage.PerformSearchFor(correctBattleTag);
                Assert.True(homePage.ProfileStatsAreVisible());
            }
        }

        [Fact]
        public void ValidationMessageIsVisible_ForInvalidBattleTag()
        {
            const string invalidBattleTag = "^^";

            using (var homePage = new HomePageObject(_webDriverUri))
            {
                homePage.PerformSearchFor(invalidBattleTag);
                Assert.True(homePage.ValidationMessageVisible());
            }
        }
    }
}
