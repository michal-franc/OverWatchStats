using Newtonsoft.Json;

namespace OverwatchStats.Web.OverwatchApi.Dto
{
    // NewtonSoft.Json is leaking here
    // Ideally there should be two representations
    // One used for serialization / deserialization and other for domain logic
    public class ProfileStats
    {
        [JsonProperty(PropertyName = "Medals-Gold")]
        public int MedalsGold { get; set; }

        [JsonProperty(PropertyName = "Medals-Silver")]
        public int MedalsSilver { get; set; }

        [JsonProperty(PropertyName = "Medals-Bronze")]
        public int MedalsBronze { get; set; }
    }
}