using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rush01.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("temp_c"), JsonConverter(typeof(DoubleConverter))]
        public double TemperatureC => TemperatureK - 273.15;

        [JsonPropertyName("temp_f"), JsonConverter(typeof(DoubleConverter))]
        public double TemperatureF => TemperatureC / 0.5556 + 32;

        [JsonPropertyName("temp_k"), JsonConverter(typeof(DoubleConverter))]
        public double TemperatureK { get; set; }

        [JsonPropertyName("wind"), JsonConverter(typeof(DoubleConverter))]
        public double Wind { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
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
