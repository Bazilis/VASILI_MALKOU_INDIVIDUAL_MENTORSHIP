using BLL.Dto;
using BLL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace BLL.Services
{
    public class WeatherStatisticalReportService : IWeatherStatisticalReport
    {
        private int _timePeriod;

        public string GetWeatherStatisticalReport(string citiesString, TimePeriodValues timePeriod)
        {
            _timePeriod = (int)timePeriod;

            var words = citiesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var inputCitiesArray = words.Select(w => w.Trim());

            var reports = inputCitiesArray.AsParallel().Select(GetResponse);

            string resultString = $"The report was generated: {DateTime.Now}. Period: {timePeriod}\n\n";

            foreach (var report in reports)
            {
                if(report.ResponseState)
                {
                    resultString += $"{report.CityName} average temperature: {report.AverageCityTemp:0.00}°C\n";
                }
                else
                {
                    resultString += $"{report.CityName}: {report.ErrorMessage}";
                }
            }

            return resultString;
        }

        private WeatherStatisticalReportInfoDto GetResponse(string cityName)
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

                    var result = GetAverageCityTemperatureForTimePeriod(apiResponse.Coord.Lat, apiResponse.Coord.Lon, _timePeriod);

                    result.CityName = cityName;

                    return result;
                }
            }
            catch (WebException ex)
            {
                return new WeatherStatisticalReportInfoDto
                {
                    CityName = cityName,
                    ResponseState = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private WeatherStatisticalReportInfoDto GetAverageCityTemperatureForTimePeriod(double lat, double lon, int timePeriod)
        {
            var startTime = timePeriod switch
            {
                1 => DateTimeOffset.Now.ToUnixTimeSeconds() - 3600,
                3 => DateTimeOffset.Now.ToUnixTimeSeconds() - 10800,
                12 => DateTimeOffset.Now.ToUnixTimeSeconds() - 43200,
                24 => DateTimeOffset.Now.ToUnixTimeSeconds() - 86400,
                _ => throw new ArgumentException(nameof(timePeriod)),
            };

            var url = $"http://history.openweathermap.org/data/2.5/history/city?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&start={startTime}&cnt={timePeriod}&type=hour&units=metric&appid=1767b42a412384f05aa99bce04d20904";

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

                    var apiResponse = JsonConvert.DeserializeObject<WeatherStatisticalReportApiResponseDto>(responseStr);

                    return new WeatherStatisticalReportInfoDto
                    {
                        AverageCityTemp = apiResponse.List.Average(x => x.Main.Temp),
                        ResponseState = true
                    };
                }
            }
            catch (WebException ex)
            {
                return new WeatherStatisticalReportInfoDto
                {
                    ResponseState = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
