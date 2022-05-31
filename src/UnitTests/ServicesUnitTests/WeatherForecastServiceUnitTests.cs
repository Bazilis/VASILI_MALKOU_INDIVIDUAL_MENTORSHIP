using BLL.Dto;
using BLL.Services;
using Xunit;

namespace UnitTests.ServicesUnitTests
{
    public class WeatherForecastServiceUnitTests
    {
        [Fact]
        public void GetWeatherForecastMethodReturnsCityValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'City Name' must not be empty.";

            var inputData = new WeatherForecastInputDataDto 
            { 
                CityName = "",
                MinNumberDays = 1,
                MaxNumberDays = 16,
                NumberOfDays = 3
            };

            var service = new WeatherForecastService();

            // act
            var result = service.GetWeatherForecast(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }

        [Fact]
        public void GetWeatherForecastMethodReturnsNumberOfDaysValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'Number Of Days' must be less than or equal to '16'.";

            var inputData = new WeatherForecastInputDataDto 
            {
                CityName = "Tashkent",
                MinNumberDays = 1,
                MaxNumberDays = 16,
                NumberOfDays = 20
            };

            var service = new WeatherForecastService();

            // act
            var result = service.GetWeatherForecast(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }

        [Fact]
        public void GetWeatherForecastMethodReturnsMinNumberDaysValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'Min Number Days' must be between 1 and 16. You entered 0.";

            var inputData = new WeatherForecastInputDataDto
            {
                CityName = "Tashkent",
                MinNumberDays = 0,
                MaxNumberDays = 16,
                NumberOfDays = 3
            };

            var service = new WeatherForecastService();

            // act
            var result = service.GetWeatherForecast(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }

        [Fact]
        public void GetWeatherForecastMethodReturnsMaxNumberDaysValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'Max Number Days' must be between 1 and 16. You entered 17.";

            var inputData = new WeatherForecastInputDataDto
            {
                CityName = "Tashkent",
                MinNumberDays = 1,
                MaxNumberDays = 17,
                NumberOfDays = 3
            };

            var service = new WeatherForecastService();

            // act
            var result = service.GetWeatherForecast(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }
    }
}
