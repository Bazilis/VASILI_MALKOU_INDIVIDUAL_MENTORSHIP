using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace UnitTests.ValidatorsUnitTests
{
    public class WeatherForecastInputDataValidatorUnitTests
    {
        [Fact]
        public void WeatherForecastInputDataValidatorHaveNoValidationError()
        {
            // arrange
            var validator = new WeatherForecastInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherForecastInputDataDto()
                {
                    CityName = "Tashkent",
                    MinNumberDays = 1,
                    MaxNumberDays = 16,
                    NumberOfDays = 3
                });

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(i => i.CityName);
            validationResult.ShouldNotHaveValidationErrorFor(i => i.MinNumberDays);
            validationResult.ShouldNotHaveValidationErrorFor(i => i.MaxNumberDays);
            validationResult.ShouldNotHaveValidationErrorFor(i => i.NumberOfDays);
        }

        [Fact]
        public void WeatherForecastInputDataValidatorHaveCityValidationError()
        {
            // arrange
            var validator = new WeatherForecastInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherForecastInputDataDto()
                {
                    CityName = "",
                    MinNumberDays = 1,
                    MaxNumberDays = 16,
                    NumberOfDays = 3
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.CityName);
        }

        [Fact]
        public void WeatherForecastInputDataValidatorHaveNumberOfDaysValidationError()
        {
            // arrange
            var validator = new WeatherForecastInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherForecastInputDataDto()
                {
                    CityName = "Tashkent",
                    MinNumberDays = 1,
                    MaxNumberDays = 16,
                    NumberOfDays = 0
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.NumberOfDays);
        }

        [Fact]
        public void WeatherForecastInputDataValidatorHaveMinNumberDaysValidationError()
        {
            // arrange
            var validator = new WeatherForecastInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherForecastInputDataDto()
                {
                    CityName = "Tashkent",
                    MinNumberDays = 0,
                    MaxNumberDays = 16,
                    NumberOfDays = 3
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.MinNumberDays);
        }

        [Fact]
        public void WeatherForecastInputDataValidatorHaveMaxNumberDaysValidationError()
        {
            // arrange
            var validator = new WeatherForecastInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherForecastInputDataDto()
                {
                    CityName = "Tashkent",
                    MinNumberDays = 1,
                    MaxNumberDays = 17,
                    NumberOfDays = 3
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.MaxNumberDays);
        }
    }
}
