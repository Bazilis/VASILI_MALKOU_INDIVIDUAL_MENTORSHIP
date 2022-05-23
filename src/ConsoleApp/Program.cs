using BLL.Services;
using ConsoleApp.Command;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            //var currentWeatherCommand = 
            //    new CurrentWeatherCommand(
            //    new CurrentWeatherService());

            //currentWeatherCommand.Execute();

            var weatherForecastCommand =
                new WeatherForecastCommand(
                new WeatherForecastService());

            weatherForecastCommand.Execute();

            Console.Read();
        }
    }
}
