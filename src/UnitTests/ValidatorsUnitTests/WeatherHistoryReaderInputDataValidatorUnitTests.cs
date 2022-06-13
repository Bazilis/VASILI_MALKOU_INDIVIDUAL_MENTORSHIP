using BLL.Dto;
using BLL.Validators;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace UnitTests.ValidatorsUnitTests
{
    public class WeatherHistoryReaderInputDataValidatorUnitTests
    {
        [Fact]
        public void WeatherHistoryReaderInputDataValidatorHaveNoValidationError()
        {
            // arrange
            var validator = new WeatherHistoryReaderInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherHistoryReaderInputDataDto()
                {
                    CityName = "Tashkent",
                    StartDate = new DateTime(2022, 6, 11),
                    EndDate = new DateTime(2022, 6, 12)
                });

            // assert
            validationResult.ShouldNotHaveValidationErrorFor(i => i.CityName);
            validationResult.ShouldNotHaveValidationErrorFor(i => i.StartDate);
            validationResult.ShouldNotHaveValidationErrorFor(i => i.EndDate);
        }

        [Fact]
        public void WeatherHistoryReaderInputDataValidatorHaveCityValidationError()
        {
            // arrange
            var validator = new WeatherHistoryReaderInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherHistoryReaderInputDataDto()
                {
                    CityName = "",
                    StartDate = new DateTime(2022, 6, 11),
                    EndDate = new DateTime(2022, 6, 12)
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.CityName);
        }

        [Fact]
        public void WeatherHistoryReaderInputDataValidatorHaveStartDateValidationError()
        {
            // arrange
            var validator = new WeatherHistoryReaderInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherHistoryReaderInputDataDto()
                {
                    CityName = "Tashkent",
                    EndDate = new DateTime(2022, 6, 12)
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.StartDate);
        }

        [Fact]
        public void WeatherHistoryReaderInputDataValidatorHaveEndDateValidationError()
        {
            // arrange
            var validator = new WeatherHistoryReaderInputDataValidator();

            // act
            var validationResult = validator.TestValidate(
                new WeatherHistoryReaderInputDataDto()
                {
                    CityName = "Tashkent",
                    StartDate = new DateTime(2022, 6, 11)
                });

            // assert
            validationResult.ShouldHaveValidationErrorFor(i => i.EndDate);
        }
    }
}
