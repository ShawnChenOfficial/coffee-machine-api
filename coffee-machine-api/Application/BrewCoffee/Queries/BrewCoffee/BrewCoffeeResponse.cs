using System;
namespace coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee
{
	public class BrewCoffeeResponse
	{
		public string Message { get; set; } = default!;
		public DateTime Prepared { get; set; }

        public BrewCoffeeResponse(string message, DateTime prepared)
        {
			this.Message = message;
			this.Prepared = prepared;
        }
	}
}

