using System;
namespace coffee_machine_api.Infrastructure.MiddlewareExtensions
{
	public static class MiddlewareExtension
	{
		public static WebApplication UseCore(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            return app;
        }
	}
}

