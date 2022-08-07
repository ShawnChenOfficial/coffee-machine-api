using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee;
using Moq;

namespace UnitTests.ApplicationTests.BrewCoffee.Queries.BrewCoffee
{
	public class BrewCoffeeQuery_Test
	{
		/// <summary>
        /// this handler will always return same result
        /// </summary>
        [Fact]
		public async void Handler_WhenNoValidationError_ReturnExpectedResult()
        {
            var mockDatetimeProvider = new Mock<IDateTimeProvider>();

            var now = DateTime.Now;

            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(now);

            var handler = new BrewCoffeeQueryHandler(mockDatetimeProvider.Object);

            var result = await handler.Handle(new BrewCoffeeQuery(), CancellationToken.None);

            Assert.True(result != null);
            Assert.True(result!.Message == "Your piping hot coffee is ready");
            Assert.True(result!.Prepared == now);
        }
	}
}

