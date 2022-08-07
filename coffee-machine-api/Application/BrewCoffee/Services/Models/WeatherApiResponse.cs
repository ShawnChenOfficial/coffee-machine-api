using System;
using System.Text.Json.Serialization;

namespace coffee_machine_api.Application.BrewCoffee.Services.Models
{
	public class WeatherApiResponse
	{
		public Main main { get; set; } = null!;
	}

	public class Main
    {
        [JsonPropertyName("temp")]
		public double Temperature { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }
        [JsonPropertyName("temp_max")]
        public double TemperatureMax { get; set; }
        [JsonPropertyName("temp_min")]
        public double TemperatureMin { get; set; }
        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    }
}

