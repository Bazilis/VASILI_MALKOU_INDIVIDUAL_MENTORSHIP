﻿using BLL.Dto;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp.Command
{
    internal class WeatherForecastCommand
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
                CityName = Console.ReadLine(), 
                NumberOfDays = Convert.ToInt32(Console.ReadLine()),
                MinNumberDays = Convert.ToInt32(_configuration["MinNumberDays"]),
                MaxNumberDays = Convert.ToInt32(_configuration["MaxNumberDays"]),
            };

            Console.WriteLine(_service.GetWeatherForecast(inputData));
        }
    }
}
