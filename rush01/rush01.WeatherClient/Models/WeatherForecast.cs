using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rush01.WeatherClient.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherInfo> WeatherInfo { get; set; }

        [JsonPropertyName("main")]
        public MainInfo MainInfo { get; set; }

        [JsonPropertyName("wind")]
        public WindInfo WindInfo { get; set; }
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

    public class DoubleConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options) =>
            reader.GetDouble();

        public override void Write(Utf8JsonWriter writer,
            double value, JsonSerializerOptions options) =>
            writer.WriteNumberValue(Math.Round(value, 2));
    }
}
