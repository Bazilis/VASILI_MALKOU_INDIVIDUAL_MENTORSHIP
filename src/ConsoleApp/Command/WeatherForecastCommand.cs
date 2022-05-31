using BLL.Dto;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp.Command
{
    internal class WeatherForecastCommand : ICommand
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IWeatherForecast _service;

        public WeatherForecastCommand(IConfigurationRoot configuration, IWeatherForecast service)
        {
            _configuration = configuration;
            _service = service;
        }

        public void Execute()
        {
            var inputData = new WeatherForecastInputDataDto 
            {
                MinNumberDays = Convert.ToInt32(_configuration["MinNumberDays"]),
                MaxNumberDays = Convert.ToInt32(_configuration["MaxNumberDays"])
            };

            Console.WriteLine("\nPlease, enter City Name and press 'Enter'...");
            inputData.CityName = Console.ReadLine();

            Console.WriteLine("Please, enter number of forecast days and press 'Enter'...");
            inputData.NumberOfDays = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(_service.GetWeatherForecast(inputData));
        }
    }
}
