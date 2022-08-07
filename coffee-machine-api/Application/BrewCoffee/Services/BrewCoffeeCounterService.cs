using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;

namespace coffee_machine_api.Application.BrewCoffee.Services
{
    /// <summary>
    /// a singleton service that helps to count the times that brew-coffee get called
    /// </summary>
	public class BrewCoffeeCounterService: IBrewCoffeeCounterService
    {
        private readonly object _lock = new ();

		private int _count = 1;

        /// <summary>
        /// </summary>
        /// <returns>
        /// if 5th call, then return true and clear the counter.
        /// otherwise, return false and count + 1</returns>
		public bool IsFifthCoffee()
        {
            lock (_lock)
            {
                if (_count == 5)
                {
                    _count = 1;
                    return true;
                }
                else
                {
                    _count++;
                    return false;
                }
            }
        }
	}
}

