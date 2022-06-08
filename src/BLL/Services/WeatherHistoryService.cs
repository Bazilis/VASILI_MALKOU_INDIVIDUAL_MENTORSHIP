using BLL.Dto;
using BLL.Dto.Options;
using BLL.Interfaces;
using DAL;
using DAL.Entities;
using Hangfire;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace BLL.Services
{
    public class WeatherHistoryService : IWeatherHistory
    {
        private bool _isSecondCall;

        private readonly AppDbContext _context;

        private readonly ILogger _log = Log.ForContext<WeatherHistoryService>();

        public WeatherHistoryService(AppDbContext context)
        {
            _context = context;
        }

        public void ManageHangfireJobs(WeatherHistoryOptions inputData)
        {
            if (!_isSecondCall)
            {
                if (inputData.CityTimers.All(x => x.TimerCronTemplate == inputData.CityTimers[0].TimerCronTemplate))
                {
                    RecurringJob.AddOrUpdate($"Job for all cities", () => JobForAllCities(inputData.CityTimers), $"{inputData.CityTimers[0].TimerCronTemplate}");
                    _log.Information("Job for all cities created");
                }
                else
                {
                    for (int i = 0; i < inputData.CityTimers.Length; i++)
                    {
                        RecurringJob.AddOrUpdate($"Job for city number {i + 1}", () => JobForOneCity(inputData.CityTimers[i].CityName), $"{inputData.CityTimers[i].TimerCronTemplate}");
                        _log.Information($"Job for city number {i + 1} created");
                    }
                }

                _isSecondCall = true;
            }
            else
            {
                _isSecondCall = false;
            }
        }

        public void JobForAllCities(CityTimer[] cityTimers)
        {
            Array.ForEach(cityTimers, x => _context.WeatherHistory.Add(GetResponse(x.CityName)));
            _context.SaveChangesAsync();
        }

        public void JobForOneCity(string cityName)
        {
            _context.WeatherHistory.Add(GetResponse(cityName));
            _context.SaveChangesAsync();
        }

        private WeatherHistory GetResponse(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=1767b42a412384f05aa99bce04d20904";

            var request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    string responseStr;

                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStr = reader.ReadToEnd();
                    }

                    var apiResponse = JsonConvert.DeserializeObject<CurrentWeatherApiResponseDto>(responseStr);

                    return new WeatherHistory
                    {
                        CityName = apiResponse.Name,
                        CityTemp = apiResponse.Main.Temp,
                        IsSuccess = true,
                        DateTime = DateTime.Now
                    };
                }
            }
            catch (WebException ex)
            {
                return new WeatherHistory
                {
                    CityName = cityName,
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    DateTime = DateTime.Now
                };
            }
        }
    }
}
