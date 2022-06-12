using BLL.Dto.Options;
using BLL.Interfaces;
using BLL.Services;
using BLL.Validators;
using DAL;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.IO;
using System.Reflection;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logFileName = $"Log_{Assembly.GetExecutingAssembly().GetName().Name}.txt";
            var logFilePath = Path.Combine(AppContext.BaseDirectory, logFileName);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger));
            builder.Services.AddSingleton(Log.Logger);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CurrentWeatherInputDataValidator>();
                fv.AutomaticValidationEnabled = false;
            });

            builder.Services.AddScoped<ICurrentWeather, CurrentWeatherService>();
            builder.Services.AddScoped<IWeatherForecast, WeatherForecastService>();
            builder.Services.AddScoped<IWeatherHistorySaver, WeatherHistorySaverService>();
            builder.Services.AddScoped<IWeatherHistoryReader, WeatherHistoryReaderService>();

            // Add Hangfire services.
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            builder.Services.AddHangfireServer();

            builder.Services.Configure<WeatherHistoryOptions>(builder.Configuration.GetSection(nameof(WeatherHistoryOptions)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseHangfireDashboard();

            using (var scope = app.Services.CreateScope())
            {
                var weatherHistoryOptionsMonitor = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<WeatherHistoryOptions>>();
                var weatherHistory = scope.ServiceProvider.GetRequiredService<IWeatherHistorySaver>();

                weatherHistoryOptionsMonitor.OnChange(weatherHistory.ManageHangfireJobs);
            }

            app.Run();
        }
    }
}
