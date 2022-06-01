using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace UnitTests.ValidatorsUnitTests
{
    public class FindCityWithMaxTempInputDataValidatorUnitTests
    {
        [Fact]
        public void FindCityWithMaxTempInputDataValidatorHaveNoValidationError()
        {
            // arrange
            var validator = new FindCityWithMaxTempInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new FindCityWithMaxTempInputDataDto()
                {
                    Cities = "Tashkent, Minsk"
                });

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(i => i.Cities);
        }

        [Fact]
        public void FindCityWithMaxTempInputDataValidatorHaveValidationError()
        {
            // arrange
            var validator = new FindCityWithMaxTempInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new FindCityWithMaxTempInputDataDto()
                {
                    Cities = ""
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.Cities);
        }
    }
}
