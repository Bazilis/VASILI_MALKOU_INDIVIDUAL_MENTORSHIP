using BLL.Dto;
using BLL.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class WeatherHistoryReaderService : IWeatherHistoryReader
    {
        private readonly AppDbContext _context;

        public WeatherHistoryReaderService(AppDbContext context)
        {
            _context = context;
        }

        public WeatherHistoryDto[] GetWeatherHistoryData(WeatherHistoryReaderInputDataDto inputData)
        {
            var weatherHistories = _context.WeatherHistory.AsNoTracking().Where(x =>
                                    x.CityName == inputData.CityName && 
                                    x.DateTime >= inputData.StartDate && 
                                    x.DateTime <= inputData.EndDate)
                                    .AsEnumerable();

            var weatherHistoriesDto = new List<WeatherHistoryDto>();

            foreach (var weatherHistory in weatherHistories)
            {
                weatherHistoriesDto.Add(new WeatherHistoryDto
                {
                    Id = weatherHistory.Id,
                    CityName = weatherHistory.CityName,
                    CityTemp = weatherHistory.CityTemp,
                    IsSuccess = weatherHistory.IsSuccess,
                    ErrorMessage = weatherHistory.ErrorMessage,
                    DateTime = weatherHistory.DateTime
                });
            }

            return weatherHistoriesDto.ToArray();
        }
    }
}
