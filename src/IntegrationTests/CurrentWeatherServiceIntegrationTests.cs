using BLL.Dto;
using BLL.Services;
using Xunit;

namespace IntegrationTests
{
    public class CurrentWeatherServiceIntegrationTests
    {
        [Fact]
        public void GetCurrentWeatherMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = @"In Tashkent(\w*)";

            var inputData = new CurrentWeatherInputDataDto { CityName = "Tashkent" };

            var service = new CurrentWeatherService();

            // act
            string result = service.GetCurrentWeather(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }
    }
}
