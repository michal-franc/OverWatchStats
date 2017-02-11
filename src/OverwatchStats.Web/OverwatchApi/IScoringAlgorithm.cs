using OverwatchStats.Web.OverwatchApi.Dto;

namespace OverwatchStats.Web.OverwatchApi
{
    public interface IScoringAlgorithm
    {
        double GetScore(ProfileStats profile);
    }
}
