using BLL.Services;
using ConsoleApp.Command;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            ConsoleKey key;

            bool exit = false;

            while (!exit)
            {
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:

                        var currentWeatherCommand =
                            new CurrentWeatherCommand(
                            new CurrentWeatherService());
                        currentWeatherCommand.Execute();

                        break;

                    case ConsoleKey.D2:

                        var weatherForecastCommand =
                            new WeatherForecastCommand(
                            new WeatherForecastService());
                        weatherForecastCommand.Execute();

                        break;
                    case ConsoleKey.D0:
                        Console.WriteLine("\nExit");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid key, try another...");
                        break;
                }
            }

            Console.Read();
        }
    }
}
