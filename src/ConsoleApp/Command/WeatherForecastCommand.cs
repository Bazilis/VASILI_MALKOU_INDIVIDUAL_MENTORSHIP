using BLL.Dto;
using BLL.Services;
using System;

namespace ConsoleApp.Command
{
    internal class WeatherForecastCommand
    {
        private readonly WeatherForecastService _service;

        public WeatherForecastCommand(WeatherForecastService service)
        {
            _service = service;
        }

        public void Execute()
        {
            var inputData = new InputDataDto 
            {
                CityName = Console.ReadLine(), 
                NumberOfDays = Convert.ToInt32(Console.ReadLine()) 
            };

            Console.WriteLine(_service.GetWeatherForecast(inputData));
        }
    }
}
