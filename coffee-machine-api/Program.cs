using coffee_machine_api.Infrastructure.MiddlewareExtensions;
using coffee_machine_api.Infrastructure.ServiceCollectionExtensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddControllers()
       .AddSwagger()
       .AddMediatR()
       .AddCore();

var app = builder.Build();

app.UseCore().Run();

