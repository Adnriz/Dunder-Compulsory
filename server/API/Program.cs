using System.Text.Json;
using API.Middleware;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;
using Service.Validators;
using Microsoft.OpenApi.Models;
using Service.Interfaces;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddOptionsWithValidateOnStart<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations()
            .Validate(options => new AppOptionsValidator().Validate(options).IsValid,
                $"{nameof(AppOptions)} validation failed");

        builder.Services.AddDbContext<WebShopContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(Environment.GetEnvironmentVariable("DbConnectionString")
                              ?? appOptions.DbConnectionString);
            options.EnableSensitiveDataLogging();
        });

        builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderDtoValidator>());
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        var app = builder.Build();

        var options = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine("APP OPTIONS: " + JsonSerializer.Serialize(options));
        
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseStatusCodePages();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
        
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WebShopContext>();
            context.Database.EnsureCreated();
        }
        
        app.Run();
    }
}