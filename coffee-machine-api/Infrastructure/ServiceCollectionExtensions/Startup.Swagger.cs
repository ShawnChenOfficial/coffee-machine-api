namespace coffee_machine_api.Infrastructure.ServiceCollectionExtensions
{
	public static class SwaggerStartup
    {
		public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.AddInfrastructure();
            return builder;
        }

        private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder;
        }

	}
}

