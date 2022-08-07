using System;
using System.Reflection;
using coffee_machine_api.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;

namespace coffee_machine_api.Infrastructure.ServiceCollectionExtensions
{
	public static class MediatRStartup
	{
		public static WebApplicationBuilder AddMediatR(this WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            builder.Services.AddValidatorsFromAssembly(assembly);
            builder.Services.AddMediatR(assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return builder;
        }
	}
}

