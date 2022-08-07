using System;
namespace coffee_machine_api.Application.BrewCoffee.Interfaces
{
	public interface IWeatherRequestService
	{
		/// <summary>
        /// send request to a third-party api to grant weather data
        /// </summary>
        /// <returns></returns>
        Task<string> SendAsync();
	}
}

