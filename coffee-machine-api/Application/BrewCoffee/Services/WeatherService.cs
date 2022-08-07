using System;
using System.Text.Json;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Services.Models;
using coffee_machine_api.Application.Exceptions;

namespace coffee_machine_api.Application.BrewCoffee.Services
{
	public class WeatherService: IWeatherService
    {
        private readonly IWeatherRequestService requestService;

        public WeatherService(IWeatherRequestService requestService)
        {
            this.requestService = requestService;
        }

        public async Task<double> GetTemperature()
        {
            var jsonString = await requestService.SendAsync();

            var result = jsonString.Deserilize();

            if (result == null || result.main == null)
            {
                throw new ResourceRequestErrorException("Failed to request weather data.");
            }

            return result.main.Temperature;
        }
    }
}

