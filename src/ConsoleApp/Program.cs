using BLL.Interfaces;
using BLL.Services;
using ConsoleApp.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Openweathermap.org API";
            Console.ForegroundColor = ConsoleColor.Green;

            string basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .AddScoped<ICommand, CurrentWeatherCommand> ()
                .AddScoped<ICommand, WeatherForecastCommand>()
                .AddScoped<ICommand, FindCityWithMaxTempCommand>()
                .AddScoped<IFindCityWithMaxTemp, FindCityWithMaxTempService>()
                .AddScoped<ICurrentWeather, CurrentWeatherService>()
                .AddScoped<IWeatherForecast, WeatherForecastService>()
                .BuildServiceProvider();

            var commands = services.GetServices<ICommand>();

            ConsoleKey key;
            bool exit = false;
            
            while (!exit)
            {
                Console.WriteLine("Please, enter the number of menu item:");
                Console.WriteLine("1 => Current weather");
                Console.WriteLine("2 => Weather forecast");
                Console.WriteLine("3 => Find city with max temperature");
                Console.WriteLine("0 => Close application");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:

                        var currentWeatherCommand = commands
                            .First(c => c.GetType().Name == nameof(CurrentWeatherCommand));
                        currentWeatherCommand.Execute();

                        break;

                    case ConsoleKey.D2:

                        var weatherForecastCommand = commands
                            .First(c => c.GetType().Name == nameof(WeatherForecastCommand));
                        weatherForecastCommand.Execute();

                        break;

                    case ConsoleKey.D3:

                        var findCityWithMaxTempCommand = commands
                            .First(c => c.GetType().Name == nameof(FindCityWithMaxTempCommand));
                        findCityWithMaxTempCommand.Execute();

                        break;

                    case ConsoleKey.D0:

                        Console.WriteLine("\nApplication closed...");
                        exit = true;

                        break;

                    default:
                        Console.WriteLine("\nInvalid key, try another...");
                        break;
                }
            }
        }
    }
}
