using BLL.Dto;
using BLL.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class WeatherForecastService
    {
        public string GetWeatherForecast(WeatherForecastInputDataDto inputData)
        {
            var inputDataValidator = new WeatherForecastInputDataValidator(inputData.MinNumberDays, inputData.MaxNumberDays);

            ValidationResult validationResult = inputDataValidator.Validate(inputData);

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Empty;

                foreach(var failure in validationResult.Errors)
                {
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
            if (temperature <= 0.0)
            {
                return "Dress warmly.";
            }
            else if (temperature > 0.0 && temperature <= 20.0)
            {
                return "It's fresh.";
            }
            else if (temperature > 20.0 && temperature <= 30.0)
            {
                return "Good weather.";
            }
            else
            {
                return "It's time to go to the beach.";
            }
        }
    }
}
