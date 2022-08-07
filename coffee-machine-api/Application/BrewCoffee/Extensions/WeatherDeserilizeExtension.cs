using System;
using System.Text.Json;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Services.Models;
using coffee_machine_api.Application.Exceptions;

namespace coffee_machine_api.Application.BrewCoffee.Services
{
	public static class WeatherDeserilizeExtension
    {
        public static WeatherApiResponse? Deserilize(this string json)
        {
            return JsonSerializer.Deserialize<WeatherApiResponse>(json);
        }
    }
}

