using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using MediatR;

namespace coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee
{
	public record BrewCoffeeQuery: IRequest<BrewCoffeeResponse>;

    public class BrewCoffeeQueryHandler : IRequestHandler<BrewCoffeeQuery, BrewCoffeeResponse>
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public BrewCoffeeQueryHandler(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public Task<BrewCoffeeResponse> Handle(BrewCoffeeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new BrewCoffeeResponse("Your piping hot coffee is ready", dateTimeProvider.GetNow()));
        }
    }
}

