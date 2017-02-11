using System.Threading.Tasks;
using OverwatchStats.Web.OverwatchApi.Dto;
using RestEase;

namespace OverwatchStats.Web.OverwatchApi
{
    public interface IOverwatchApiClientAsync
    {
        [Get("/pc/eu/{battletag}/competitive/allHeroes/")]
        Task<ProfileStats> GetProfileStatsAsync([Path] string battletag);
    }
}
