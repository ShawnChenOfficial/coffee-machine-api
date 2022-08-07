using System;
using coffee_machine_api.Application.BrewCoffee.Services;

namespace UnitTests.ApplicationTests.BrewCoffee.Services.BrewCoffeeConterService
{
	public class BrewCoffeeCounterService_IsFifthCoffee_Test
    {
        /// <summary>
        /// as long as the 5th, should return true
        /// </summary>
        [Fact]
        public void IsFifthCoffee_When5th_ReturnTrue()
        {
            var service = new BrewCoffeeCounterService();

            var anyFalse = false;

            for (int i = 1; i < 101; i++)
            {
                if (!service.IsFifthCoffee() && i % 5 == 0)
                {
                    anyFalse = true;
                    break;
                }
            }

            Assert.False(anyFalse);
        }

        [Fact]
        public void IsFifthCoffee_WhenNot5th_ReturnFalse()
        {
            var service = new BrewCoffeeCounterService();

            var anyTrue = false;

            for (int i = 1; i < 101; i++)
            {
                if (service.IsFifthCoffee() && i % 5 != 0)
                {
                    anyTrue = true;
                    break;
                }
            }

            Assert.False(anyTrue);
        }
    }
}

