using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rush01.Models
{
    public class OpenWeatherMapResponse
    {
        [JsonPropertyName("weather")]
        public List<WeatherInfo> WeatherInfo { get; set; }

        [JsonPropertyName("main")]
        public MainInfo MainInfo { get; set; }

        [JsonPropertyName("wind")]
        public WindInfo WindInfo { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public double TempKelvin => MainInfo.Temp;
        public int Pressure => MainInfo.Pressure;
        public int Humidity => MainInfo.Humidity;
        public string Description => WeatherInfo[0].Description;
        public double WindSpeed => WindInfo.Speed;
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }
}
