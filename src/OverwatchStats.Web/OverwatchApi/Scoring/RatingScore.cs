using OverwatchStats.Web.OverwatchApi.Dto;

namespace OverwatchStats.Web.OverwatchApi.Scoring
{
    public class RatingScore : IScoringAlgorithm
    {
        private double MedalsScore(ProfileStats r)
        {
            return r.MedalsGold*3 + r.MedalsSilver*2 + r.MedalsBronze;
        }

        public double GetScore(ProfileStats profile)
        {
            return MedalsScore(profile);
        }
    }
}
