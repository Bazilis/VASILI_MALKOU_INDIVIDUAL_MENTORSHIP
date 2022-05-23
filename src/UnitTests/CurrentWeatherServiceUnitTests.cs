using BLL.Dto;
using BLL.Services;
using Xunit;

namespace UnitTests
{
    public class CurrentWeatherServiceUnitTests
    {
        [Fact]
        public void GetCurrentWeatherMethodReturnsValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'City Name' must not be empty.";

            var inputData = new CurrentWeatherInputDataDto { CityName = "" };

            var service = new CurrentWeatherService();

            // act
            var result = service.GetCurrentWeather(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }
    }
}
