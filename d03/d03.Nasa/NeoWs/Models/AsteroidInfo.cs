using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidInfo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        public double Kilometers => CloseApproachData[0].MissDistance.Kilometers;

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; }
    }

    public class CloseApproachData
    {
        [JsonPropertyName("miss_distance")]
        public MissDistance MissDistance { get; set; }
    }

    public class MissDistance
    {
        [JsonPropertyName("kilometers")]
        public double Kilometers { get; set; }
    }
}
