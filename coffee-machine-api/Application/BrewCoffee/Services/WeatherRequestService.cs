using System;
using System.Text.Json;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Services.Models;
using coffee_machine_api.Application.Exceptions;

namespace coffee_machine_api.Application.BrewCoffee.Services
{
	public class WeatherRequestService: IWeatherRequestService
    {
		private readonly IConfiguration config;

        public WeatherRequestService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<string> SendAsync()
        {
            var client = new HttpClient();

            var baseUrl = config.GetSection("WeatherAPI:BaseUrl").Value.ToString();
            var apiKey = config.GetSection("WeatherAPI:Key").Value.ToString();
            var lat = config.GetSection("WeatherAPI:Lat").Value.ToString();
            var lon = config.GetSection("WeatherAPI:Lon").Value.ToString();
            var unit = config.GetSection("WeatherAPI:Unit").Value.ToString();

            var response = await client.GetAsync($"{baseUrl}?lat={lat}&lon={lon}&units={unit}&appid={apiKey}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}

