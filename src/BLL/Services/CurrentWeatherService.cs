using BLL.Dto;
using BLL.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class CurrentWeatherService
    {
        public string GetCurrentWeather(CurrentWeatherInputDataDto inputData)
        {
            var inputDataValidator = new CurrentWeatherInputDataValidator();

            ValidationResult validationResult = inputDataValidator.Validate(inputData);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors[0].ErrorMessage;
            }

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={inputData.CityName}&units=metric&appid=1767b42a412384f05aa99bce04d20904";

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

                    return $"In {apiResponse.Name} {apiResponse.Main.Temp} °C. {CommentСhoosing(apiResponse.Main.Temp)}";
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
