using BLL.Dto;
using BLL.Interfaces;
using BLL.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace BLL.Services
{
    public class FindCityWithMaxTempService : IFindCityWithMaxTemp
    {
        private double _responseTimeout;

        public string FindCityWithMaxTemp(FindCityWithMaxTempInputDataDto inputData)
        {
            _responseTimeout = inputData.ResponseTimeout;

            var inputDataValidator = new FindCityWithMaxTempInputDataValidator();

            ValidationResult validationResult = inputDataValidator.Validate(inputData);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors[0].ErrorMessage;
            }

            var words = inputData.Cities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var inputCitiesArray = words.Select(w => w.Trim());

            var responsesInfo = inputCitiesArray.AsParallel().Select(GetResponse);

            double highestTemp = 0.0;
            string highestTempCity = string.Empty;
            int successfulRequestsCount = 0;
            int failedRequestsCount = 0;
            int canceledRequestsCount = 0;
            string resultString = string.Empty;

            foreach (var responseInfo in responsesInfo)
            {
                if (responseInfo.CityTemp > highestTemp)
                {
                    highestTemp = responseInfo.CityTemp;
                    highestTempCity = responseInfo.CityName;
                }

                if (inputData.IsStatisticsOutputEnable)
                {
                    switch (responseInfo.ResponseState)
                    {
                        case ResponseState.Successful:
                            resultString += $"City: {responseInfo.CityName} : {responseInfo.CityTemp}°C. Timer: {responseInfo.ResponseTimeMs}ms.\n";
                            successfulRequestsCount++;
                            break;

                        case ResponseState.Failed:
                            resultString += $"City: {responseInfo.CityName}. Error: {responseInfo.ErrorMessage} Timer: {responseInfo.ResponseTimeMs}ms.\n";
                            failedRequestsCount++;
                            break;

                        case ResponseState.Canceled:
                            resultString += $"Weather request for {responseInfo.CityName} was canceled due to a timeout. Timer: {responseInfo.ResponseTimeMs}ms.\n";
                            canceledRequestsCount++;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    switch (responseInfo.ResponseState)
                    {
                        case ResponseState.Successful:
                            successfulRequestsCount++;
                            break;

                        case ResponseState.Failed:
                            failedRequestsCount++;
                            break;

                        case ResponseState.Canceled:
                            canceledRequestsCount++;
                            break;

                        default:
                            break;
                    }
                }
            }

            if (successfulRequestsCount > 0)
            {
                resultString += $"\nCity with the highest temperature {highestTemp}°C: {highestTempCity}. Successful request count: {successfulRequestsCount}, failed: {failedRequestsCount}, canceled: {canceledRequestsCount}.\n";
            }
            else
            {
                resultString += $"\nNo successful requests. Failed requests count: {failedRequestsCount}, canceled: {canceledRequestsCount}.\n";
            }

            return resultString;
        }

        private FindCityResponseInfoDto GetResponse(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=1767b42a412384f05aa99bce04d20904";

            var request = (HttpWebRequest)WebRequest.Create(url);

            var timer = Stopwatch.StartNew();

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    timer.Stop();

                    if (timer.Elapsed.TotalMilliseconds < _responseTimeout)
                    {
                        return new FindCityResponseInfoDto
                        {
                            CityName = cityName,
                            ResponseTimeMs = timer.Elapsed.TotalMilliseconds,
                            ResponseState = ResponseState.Canceled
                        };
                    }

                    string responseStr;

                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseStr = reader.ReadToEnd();
                    }

                    var apiResponse = JsonConvert.DeserializeObject<CurrentWeatherApiResponseDto>(responseStr);

                    return new FindCityResponseInfoDto
                    {
                        CityName = apiResponse.Name,
                        CityTemp = apiResponse.Main.Temp,
                        ResponseTimeMs = timer.Elapsed.TotalMilliseconds,
                        ResponseState = ResponseState.Successful
                    };
                }
            }
            catch (WebException ex)
            {
                return new FindCityResponseInfoDto
                {
                    CityName = cityName,
                    ResponseTimeMs = timer.Elapsed.TotalMilliseconds,
                    ResponseState = ResponseState.Failed,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
