using BLL.Dto;
using BLL.Services;
using System;

namespace ConsoleApp.Command
{
    internal class CurrentWeatherCommand : ICommand
    {
        private readonly CurrentWeatherService _service;

        public CurrentWeatherCommand(CurrentWeatherService service)
        {
            _service = service;
        }

        public void Execute()
        {
            Console.WriteLine("\nPlease, enter City Name and press 'Enter'...");
            var inputData = new CurrentWeatherInputDataDto { CityName = Console.ReadLine() };

            Console.WriteLine(_service.GetCurrentWeather(inputData));
        }
    }
}
