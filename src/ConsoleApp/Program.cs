using BLL.Services;
using ConsoleApp.Command;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Openweathermap.org API";
            Console.ForegroundColor = ConsoleColor.Green;

            var services = new ServiceCollection()
                .AddScoped<CurrentWeatherCommand, CurrentWeatherCommand>()
                .AddScoped<WeatherForecastCommand, WeatherForecastCommand>()
                .AddScoped<CurrentWeatherService, CurrentWeatherService>()
                .AddScoped<WeatherForecastService, WeatherForecastService>()
                .BuildServiceProvider();

            ConsoleKey key;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Please, enter the number of menu item:");
                Console.WriteLine("1 => Current weather");
                Console.WriteLine("2 => Weather forecast");
                Console.WriteLine("0 => Close application");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:

                        var currentWeatherCommand = services
                            .GetRequiredService<CurrentWeatherCommand>();
                        currentWeatherCommand.Execute();

                        break;

                    case ConsoleKey.D2:

                        var weatherForecastCommand = services
                            .GetRequiredService<WeatherForecastCommand>();
                        weatherForecastCommand.Execute();

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
