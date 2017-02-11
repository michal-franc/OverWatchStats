using System;
using System.Threading.Tasks;
using OverwatchStats.Web.Api;
using OverwatchStats.Web.OverwatchApi.Dto;
using OverwatchStats.Web.OverwatchApi.Validators;

namespace OverwatchStats.Web.OverwatchApi
{
    public class OverwatchApiClientAsync : IProfileStatsProviderAsync
    {
        private readonly IOverwatchApiClientAsync _apiClientAsync;

        public OverwatchApiClientAsync(IOverwatchApiClientAsync apiClientAsync)
        {
            if (apiClientAsync == null) throw new ArgumentNullException(nameof(apiClientAsync), "No api client provided.");

            _apiClientAsync = apiClientAsync;
        }

        public async Task<ProfileStats> GetProfileStats(string battleTag)
        {
            if (battleTag == null) throw new ArgumentNullException(nameof(battleTag), "No battleTag parameter provided.");

            // Decided to use static class for simplicity
            // This complicates testing a bit,
            // validation on this layer is ok especialy when this function is quite thin
            // To change this I would use MediatR to generate in-proc command / handler system
            // and Validation would be part of the pipeline as middleware
            if (BattleTagValidator.IsInvalid(battleTag)) throw new ArgumentException("Provided battleTag is invalid.", nameof(battleTag));

            // This has to be moved somewhere else
            // officialy battle net uses # but the api provider used in theses examples expects -
            // it is good to expose the consumer to the official data and hide the api quirks
            var adaptedBattleTag = battleTag.Replace('#', '-');

            var response = await _apiClientAsync.GetProfileStatsAsync(adaptedBattleTag);
            return response;
        }
    }
}