using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace UnitTests
{
    public class InputDataValidatorUnitTests
    {
        [Fact]
        public void InputDataValidatorHaveNoValidationError()
        {
            // arrange
            var validator = new InputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new InputDataDto()
                {
                    CityName = "Tashkent"
                });

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(i => i.CityName);
        }

        [Fact]
        public void InputDataValidatorHaveValidationError()
        {
            // arrange
            var validator = new InputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new InputDataDto()
                {
                    CityName = "F"
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.CityName);
        }
    }
}
