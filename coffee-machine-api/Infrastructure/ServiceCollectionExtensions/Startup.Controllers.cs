using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Providers;
using coffee_machine_api.Application.BrewCoffee.Services;
using coffee_machine_api.Application.ExceptionFilterAttibutes;

namespace coffee_machine_api.Infrastructure.ServiceCollectionExtensions
{
	public static class ControllerStartup
	{
		public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
        {
            builder.AddInfrastructure();
            return builder;
        }

        private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            return builder;
        }

	}
}

