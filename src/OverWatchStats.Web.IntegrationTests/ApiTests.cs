using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using OverwatchStats.Web;
using OverWatchStats.Web.IntegrationTests.OverWatchFakedApi;
using Xunit;

namespace OverWatchStats.Web.IntegrationTests
{
    public class ApiTests
    {
        private readonly HttpClient _client;

        public ApiTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task ApiProfileStats_Returns200_AndReturnsData()
        {
            using (new FakedApiServer())
            {
                var response = await _client.GetAsync("/api/profileStats?battleTag=Lamzorjek%232324");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var content = await response.Content.ReadAsStringAsync();

                Assert.Contains("medalsGold\":10", content);
            }
        }

        [Fact]
        public async Task ApiProfileStats_Returns404()
        {
            var response = await _client.GetAsync("/api/notfound");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task ApiProfileStats_Returns400_WhenBattleTagNull()
        {
            var response = await _client.GetAsync("/api/profileStats");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ApiProfileStats_Returns400_WhenBattleTagInvalid()
        {
            var response = await _client.GetAsync("/api/profileStats?battleTag=^^");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
