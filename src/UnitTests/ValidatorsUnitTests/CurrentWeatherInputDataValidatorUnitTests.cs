using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace UnitTests.ValidatorsUnitTests
{
    public class CurrentWeatherInputDataValidatorUnitTests
    {
        [Fact]
        public void CurrentWeatherInputDataValidatorHaveNoValidationError()
        {
            // arrange
            var validator = new CurrentWeatherInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new CurrentWeatherInputDataDto()
                {
                    CityName = "Tashkent"
                });

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(i => i.CityName);
        }

        [Fact]
        public void CurrentWeatherInputDataValidatorHaveValidationError()
        {
            // arrange
            var validator = new CurrentWeatherInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new CurrentWeatherInputDataDto()
                {
                    CityName = ""
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.CityName);
        }
    }
}
