using BLL.Interfaces;
using BLL.Services;
using BLL.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CurrentWeatherInputDataValidator>();
                fv.AutomaticValidationEnabled = false;
            });

            builder.Services.AddScoped<ICurrentWeather, CurrentWeatherService>();
            builder.Services.AddScoped<IWeatherForecast, WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
