using System;
using System.Text;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using MediatR;

namespace coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee
{
	public record BrewCoffeeQuery: IRequest<BrewCoffeeResponse>;

    public class BrewCoffeeQueryHandler : IRequestHandler<BrewCoffeeQuery, BrewCoffeeResponse>
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IWeatherService weatherApiService;
        private readonly IConfiguration config;

        public BrewCoffeeQueryHandler(IDateTimeProvider dateTimeProvider, IWeatherService weatherApiService, IConfiguration config)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.weatherApiService = weatherApiService;
            this.config = config;
        }

        public async Task<BrewCoffeeResponse> Handle(BrewCoffeeQuery request, CancellationToken cancellationToken)
        {
            var temperature = await weatherApiService.GetTemperature();
            var maxHotCoffeeServeTemperature = double.Parse(this.config.GetSection("HotCoffee:MaxServeTemp").Value);

            var message = new StringBuilder("Your piping hot coffee is ready");


            if (temperature > maxHotCoffeeServeTemperature)
            {
                message.Clear();
                message.Append("Your refreshing iced coffee is ready");
            }

            return new BrewCoffeeResponse(message.ToString(), dateTimeProvider.GetNow());
        }
    }
}

