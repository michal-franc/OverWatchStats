using Microsoft.AspNetCore.Builder;
using Nancy;
using Nancy.Owin;
using OverwatchStats.Web.OverwatchApi.Dto;

namespace OverWatchStats.Web.IntegrationTests.OverWatchFakedApi
{
    public sealed class ProfileStatsEndpoint : NancyModule
    {
        private readonly ProfileStats _dummyApiProfileStats = new ProfileStats {
                MedalsGold =  10,
                MedalsBronze = 5,
                MedalsSilver = 1
            };

        public ProfileStatsEndpoint()
        {
            Get("/pc/eu/{battletag}/competitive/allHeroes/", args => Response.AsJson(_dummyApiProfileStats));
        }
    }

    public class OverWatchFakeNancy
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }
}
