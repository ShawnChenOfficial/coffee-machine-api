using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;

namespace coffee_machine_api.Application.BrewCoffee.Providers
{
	public class DateTimeProvider: IDateTimeProvider
    {

        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}

