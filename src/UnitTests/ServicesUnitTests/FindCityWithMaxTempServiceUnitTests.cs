using BLL.Dto;
using BLL.Services;
using Xunit;

namespace UnitTests.ServicesUnitTests
{
    public class FindCityWithMaxTempServiceUnitTests
    {
        [Fact]
        public void FindCityWithMaxTempMethodReturnsValidationErrorMessage()
        {
            // arrange
            string errorMessage = "'Cities' must not be empty.";

            var inputData = new FindCityWithMaxTempInputDataDto { Cities = "" };

            var service = new FindCityWithMaxTempService();

            // act
            var result = service.FindCityWithMaxTemp(inputData);

            // assert
            Assert.Equal(errorMessage, result);
        }
    }
}
