using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rush01.WeatherClient.Models
{
    /// <summary>
    /// Weather forecast model from OpenWeatherAPI
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// The name of the city
        /// </summary>
        /// <example>Moscow</example>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Weather info array
        /// </summary>
        [JsonPropertyName("weather")]
        public List<WeatherInfo> WeatherInfo { get; set; }

        /// <summary>
        /// Main info about weather including temperature, pressure and humidity
        /// </summary>
        [JsonPropertyName("main")]
        public MainInfo MainInfo { get; set; }

        /// <summary>
        /// Wind information
        /// </summary>
        [JsonPropertyName("wind")]
        public WindInfo WindInfo { get; set; }
    }

    /// <summary>
    /// Contains main information including temp, pressure, humidity
    /// </summary>
    public class MainInfo
    {
        /// <summary>
        /// Temperature in Celsius
        /// </summary>
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Pressure in hPa
        /// </summary>
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        /// <summary>
        /// Humidity in percents
        /// </summary>
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    /// <summary>
    /// Contains weather description
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// Description of weather
        /// </summary>
        /// <example>Sunny</example>
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Contains wind information
    /// </summary>
    public class WindInfo
    {
        /// <summary>
        /// Wind speed in meters per second
        /// </summary>
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
