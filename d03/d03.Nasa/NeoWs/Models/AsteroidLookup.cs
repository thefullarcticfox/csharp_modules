using System;
using System.Text.Json.Serialization;

namespace d03.Nasa.NeoWs.Models
{
    public class AsteroidLookup
    {
        [JsonPropertyName("neo_reference_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nasa_jpl_url")]
        public Uri NasaUrl { get; set; }

        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardous { get; set; }

        [JsonPropertyName("orbital_data")]
        public OrbitalData OrbitalData { get; set; }

        public string OrbitClassType => OrbitalData.OrbitClass.OrbitClassType;

        public string OrbitClassDescription => OrbitalData.OrbitClass.OrbitClassDescription;

        public override string ToString() =>
            $"Asteroid {Name}, SPK-ID: {Id}\n" +
            $"{(IsPotentiallyHazardous ? "IS POTENTIALLY HAZARDOUS!\n" : "")}" +
            $"Classification: {OrbitClassType}, {OrbitClassDescription}\nUrl: {NasaUrl}.";
    }

    public class OrbitalData
    {
        [JsonPropertyName("orbit_class")]
        public OrbitClass OrbitClass { get; set; }
    }

    public class OrbitClass
    {
        [JsonPropertyName("orbit_class_type")]
        public string OrbitClassType { get; set; }

        [JsonPropertyName("orbit_class_description")]
        public string OrbitClassDescription { get; set; }
    }
}
