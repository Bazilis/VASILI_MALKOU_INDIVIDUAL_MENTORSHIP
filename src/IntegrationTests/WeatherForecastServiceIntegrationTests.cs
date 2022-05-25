using BLL.Dto;
using BLL.Services;
using Xunit;

namespace IntegrationTests
{
    public class WeatherForecastServiceIntegrationTests
    {
        [Fact]
        public void GetWeatherForecastMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = "Tashkent weather forecast:\nDay 1: " + @"(\w*)" + "\nDay 2: " + @"(\w*)";

            var inputData = new WeatherForecastInputDataDto
            {
                CityName = "Tashkent",
                MinNumberDays = 1,
                MaxNumberDays = 16,
                NumberOfDays = 2
            };

            var service = new WeatherForecastService();

            // act
            string result = service.GetWeatherForecast(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }
    }
}
