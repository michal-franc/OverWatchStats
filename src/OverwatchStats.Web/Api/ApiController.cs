using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OverwatchStats.Web.Api
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IProfileStatsProviderAsync _dataProvider;

        public ApiController(IProfileStatsProviderAsync dataProvider)
        {
            _dataProvider = dataProvider;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        [Route("profileStats")]
        public async Task<ObjectResult> GetProfileStats(string battleTag)
        {
            if (string.IsNullOrWhiteSpace(battleTag)) return BadRequest("battleTag parameter has not been supplied");

            try
            {
                var profileStats = await _dataProvider.GetProfileStats(battleTag);
                return Ok(profileStats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
