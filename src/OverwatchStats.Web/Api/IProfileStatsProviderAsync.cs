using System.Threading.Tasks;
using OverwatchStats.Web.OverwatchApi.Dto;

namespace OverwatchStats.Web.Api
{
    public interface IProfileStatsProviderAsync
    {
        Task<ProfileStats> GetProfileStats(string battleTag);
    }
}