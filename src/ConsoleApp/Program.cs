using BLL.Dto;
using BLL.Services;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            var inputData = new InputDataDto { CityName = Console.ReadLine() };

            var service = new ForecastService();

            Console.WriteLine(service.GetForecast(inputData));
        }
    }
}
