using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Services;
using coffee_machine_api.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.ApplicationTests.BrewCoffee.Services.WeatherApiService.GetTemperature
{
	public class WeatherApiService_GetTemperature_Test
	{
        [Fact]
		public async void GetTemperature_WhenMissingAnyConfig_ThrowException()
        {
            var config = new Mock<IConfiguration>().Object;

            var requestService = new Mock<WeatherRequestService>(config).Object;

            var service = new coffee_machine_api.Application.BrewCoffee.Services.WeatherService(requestService);

            await Assert.ThrowsAsync<NullReferenceException>(() => service.GetTemperature());
        }

        [Fact]
        public async void GetTemperature_WhenHaveFullConfig_ThrowNoException()
        {
            var config = MockConfiguration();

            var requestService = new Mock<WeatherRequestService>(config).Object;

            var weatherService = new coffee_machine_api.Application.BrewCoffee.Services.WeatherService(requestService);

            var exception = await Record.ExceptionAsync(async () =>
            {
                await weatherService.GetTemperature();
            });

            Assert.Null(exception);
        }

        [Fact]
        public async void GetTemperature_WhenUnauthorizedRequest_ThrowRequestErrorException()
        {
            var config = MockConfiguration(false);

            var requestService = new Mock<WeatherRequestService>(config).Object;

            var weatherService = new coffee_machine_api.Application.BrewCoffee.Services.WeatherService(requestService);

            await Assert.ThrowsAsync<ResourceRequestErrorException>(async () => await weatherService.GetTemperature());
        }

        private IConfiguration MockConfiguration(bool authorizedRequest = true)
        {
			var mock = new Mock<IConfiguration>();
			mock.Setup(s => s.GetSection("WeatherAPI:BaseUrl").Value).Returns("https://api.openweathermap.org/data/2.5/weather");
            mock.Setup(s => s.GetSection("WeatherAPI:Lat").Value).Returns("-46.4249983");
            mock.Setup(s => s.GetSection("WeatherAPI:Lon").Value).Returns("168.30999876");
            mock.Setup(s => s.GetSection("WeatherAPI:Unit").Value).Returns("metric");

            if(authorizedRequest)
                mock.Setup(s => s.GetSection("WeatherAPI:Key").Value).Returns("127ab790b2d86bcf3eb6fb805046363a");
            else
                mock.Setup(s => s.GetSection("WeatherAPI:Key").Value).Returns("fake-api-key");

            return mock.Object;
        }
	}
}
