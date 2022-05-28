using BLL.Dto;
using BLL.Interfaces;
using BLL.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class WeatherForecastService : IWeatherForecast
    {
        public string GetWeatherForecast(WeatherForecastInputDataDto inputData)
        {
            var inputDataValidator = new WeatherForecastInputDataValidator();

            ValidationResult validationResult = inputDataValidator.Validate(inputData);

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Empty;

                foreach(var failure in validationResult.Errors)
                {
                    if (validationResult.Errors.Count == 1)
                        errorMessage = failure.ErrorMessage;
                    else
                        errorMessage += $"{failure.ErrorMessage}\n";
                }

                return errorMessage;
            }

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={inputData.CityName}&appid=1767b42a412384f05aa99bce04d20904";

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

                    return GetWeatherForecastByCityCoordinates(apiResponse.Coord.Lat, apiResponse.Coord.Lon, inputData.NumberOfDays);
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }

        private string GetWeatherForecastByCityCoordinates(double lat, double lon, int cnt)
        {
            var url = $"http://pro.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt}&units=metric&appid=1767b42a412384f05aa99bce04d20904";

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

                    var apiResponse = JsonConvert.DeserializeObject<WeatherForecastApiResponseDto>(responseStr);

                    string resultString = $"{apiResponse.City.Name} weather forecast:\n";

                    int dayNumber = 1;

                    foreach(var tempInfo in apiResponse.List)
                    {
                        if (apiResponse.List[apiResponse.List.Length - 1] == tempInfo)
                            resultString += $"Day {dayNumber}: {tempInfo.Temp.Day} °C. {CommentСhoosing(tempInfo.Temp.Day)}";
                        else
                            resultString += $"Day {dayNumber}: {tempInfo.Temp.Day} °C. {CommentСhoosing(tempInfo.Temp.Day)}\n";

                        dayNumber++;
                    }

                    return resultString;
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
        }

        private string CommentСhoosing(double temperature)
        {
            return temperature switch
            {
                <= 0.0 => "Dress warmly.",
                > 0.0 and <= 20.0 => "It's fresh.",
                > 20.0 and <= 30.0 => "Good weather.",
                > 30.0 => "It's time to go to the beach.",
                _ => throw new ArgumentOutOfRangeException(nameof(temperature)),
            };
        }
    }
}
