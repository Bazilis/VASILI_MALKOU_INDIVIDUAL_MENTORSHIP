using BLL.Dto;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp.Command
{
    internal class FindCityWithMaxTempCommand : ICommand
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IFindCityWithMaxTemp _service;

        public FindCityWithMaxTempCommand(IConfigurationRoot configuration, IFindCityWithMaxTemp service)
        {
            _configuration = configuration;
            _service = service;
        }

        public void Execute()
        {
            var inputData = new FindCityWithMaxTempInputDataDto
            {
                IsStatisticsOutputEnable = Convert.ToBoolean(_configuration["IsStatisticsOutputEnable"])
            };

            Console.WriteLine("\nPlease, enter cities names splitted by comma and press 'Enter'...");
            inputData.Cities = Console.ReadLine();

            Console.WriteLine(_service.FindCityWithMaxTemp(inputData));
        }
    }
}
