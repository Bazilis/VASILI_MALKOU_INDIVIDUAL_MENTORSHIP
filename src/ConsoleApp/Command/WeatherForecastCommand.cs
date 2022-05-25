using BLL.Dto;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp.Command
{
    internal class WeatherForecastCommand : ICommand
    {
        private readonly IConfigurationRoot _configuration;

        private readonly WeatherForecastService _service;

        public WeatherForecastCommand(WeatherForecastService service)
        {
            string basePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

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
