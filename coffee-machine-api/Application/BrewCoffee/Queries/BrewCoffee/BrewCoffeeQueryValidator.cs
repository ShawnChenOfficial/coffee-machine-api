using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.Exceptions;
using FluentValidation;

namespace coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee
{
	public class BrewCoffeeQueryValidator: AbstractValidator<BrewCoffeeQuery>
	{
		public BrewCoffeeQueryValidator(IBrewCoffeeCounterService counter, IDateTimeProvider dateTimeProvider)
		{
            var day = dateTimeProvider.GetNow().Day;

            RuleFor(r => r)
				.Must(m => day != 1)
				.WithState(x => new StatusCodeBasedValidationException(StatusCodes.Status418ImATeapot))
				.DependentRules(() =>
                {
					RuleFor(r => r)
					.Must(m => !counter.IsFifthCoffee())
					.WithState(x => new StatusCodeBasedValidationException(StatusCodes.Status503ServiceUnavailable));
                });
		}
	}
}

