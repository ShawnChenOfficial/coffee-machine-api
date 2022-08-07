using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Providers;
using coffee_machine_api.Application.BrewCoffee.Services;
using coffee_machine_api.Application.ExceptionFilterAttibutes;

namespace coffee_machine_api.Infrastructure.ServiceCollectionExtensions
{
	public static class CoreStartup
	{
		public static WebApplicationBuilder AddCore(this WebApplicationBuilder builder)
        {
            builder.AddApplication();
            return builder;
        }

        private static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IBrewCoffeeCounterService, BrewCoffeeCounterService>();
            builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IWeatherService, WeatherService>();
            builder.Services.AddTransient<IWeatherRequestService, WeatherRequestService>();
            return builder;
        }
	}
}

