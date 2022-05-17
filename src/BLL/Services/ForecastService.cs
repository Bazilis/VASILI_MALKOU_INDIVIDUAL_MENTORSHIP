using BLL.Dto;
using BLL.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace BLL.Services
{
    public class ForecastService
    {
        public string GetForecast(InputDataDto inputData)
        {
            var inputDataValidator = new InputDataValidator();

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

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto>(responseStr);

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
