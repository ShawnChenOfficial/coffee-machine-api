using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.ApplicationTests.BrewCoffee.Queries.BrewCoffee
{
    /// <summary>
    /// this test class does not consider when there is a validation failure
    /// </summary>
	public class BrewCoffeeQuery_Test
	{
		/// <summary>
        /// when less or equal than 30 degree, return the message represent hot coffee
        /// </summary>
        [Fact]
		public async void Handler_WhenTempLessOrEqual30Degree_ReturnHotCoffee()
        {
            var now = DateTime.Now;

            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(s => s.GetTemperature()).Returns(Task.FromResult(30d));

            var datetimeProvider = MockIDateTimeProvider(now);
            var config = MockIConfiguration();

            var handler = new BrewCoffeeQueryHandler(datetimeProvider, mockWeatherService.Object, config);

            var result = await handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            Assert.True(result != null);
            Assert.True(result!.Message == "Your piping hot coffee is ready");
            Assert.True(result!.Prepared == now);
        }

        /// <summary>
        /// when greater than 30 degree, return the message represent iced coffee
        /// </summary>
        [Fact]
        public async void Handler_WhenTempGreater30Degree_ReturnHotCoffee()
        {
            var now = DateTime.Now;

            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(s => s.GetTemperature()).Returns(Task.FromResult(31d));

            var datetimeProvider = MockIDateTimeProvider(now);
            var config = MockIConfiguration();

            var handler = new BrewCoffeeQueryHandler(datetimeProvider, mockWeatherService.Object, config);

            var result = await handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            Assert.True(result != null);
            Assert.True(result!.Message == "Your refreshing iced coffee is ready");
            Assert.True(result!.Prepared == now);
        }

        private IConfiguration MockIConfiguration()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(s => s.GetSection("HotCoffee:MaxServeTemp").Value).Returns("30");
            return mock.Object;
        }

        private IDateTimeProvider MockIDateTimeProvider(DateTime now)
        {
            var mockDatetimeProvider = new Mock<IDateTimeProvider>();
            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(now);

            return mockDatetimeProvider.Object;
        }
    }
}

