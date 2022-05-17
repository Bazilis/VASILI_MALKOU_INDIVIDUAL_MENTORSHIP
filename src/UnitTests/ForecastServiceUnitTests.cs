using BLL.Dto;
using BLL.Services;
using Xunit;

namespace UnitTests
{
    public class ForecastServiceUnitTests
    {
        [Fact]
        public void GetForecastMethodReturnsValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'City Name' must not be empty.";

            var inputData = new InputDataDto { CityName = "" };

            var service = new ForecastService();

            // act
            var result = service.GetForecast(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }
    }
}
