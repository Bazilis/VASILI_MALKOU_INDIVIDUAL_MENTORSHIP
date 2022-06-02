using BLL.Dto;
using BLL.Services;
using Xunit;

namespace IntegrationTests
{
    public class FindCityWithMaxTempServiceIntegrationTests
    {
        [Fact]
        public void FindCityWithMaxTempStatisticsEnableMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = @"(?s)City:(.*)City with the highest temperature(\w*)";

            var inputData = new FindCityWithMaxTempInputDataDto
            {
                Cities = "Minsk, Brest, Homel, Moscow, New York",
                IsStatisticsOutputEnable = true
            };

            var service = new FindCityWithMaxTempService();

            // act
            string result = service.FindCityWithMaxTemp(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }

        [Fact]
        public void FindCityWithMaxTempStatisticsEnableErrorMessageMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = @"(?s)City:(.*)No successful requests.(\w*)";

            var inputData = new FindCityWithMaxTempInputDataDto
            {
                Cities = "lkgdlf, dfdgk, dgpdpd",
                IsStatisticsOutputEnable = true
            };

            var service = new FindCityWithMaxTempService();

            // act
            string result = service.FindCityWithMaxTemp(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }

        [Fact]
        public void FindCityWithMaxTempStatisticsDisableMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = "City with the highest temperature" + @"(\w*)";

            var inputData = new FindCityWithMaxTempInputDataDto
            {
                Cities = "Minsk, Brest, Homel, Moscow, New York",
                IsStatisticsOutputEnable = false
            };

            var service = new FindCityWithMaxTempService();

            // act
            string result = service.FindCityWithMaxTemp(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }

        [Fact]
        public void FindCityWithMaxTempStatisticsDisableErrorMessageMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = "No successful requests." + @"(\w*)";

            var inputData = new FindCityWithMaxTempInputDataDto
            {
                Cities = "lkgdlf, dfdgk, dgpdpd",
                IsStatisticsOutputEnable = false
            };

            var service = new FindCityWithMaxTempService();

            // act
            string result = service.FindCityWithMaxTemp(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }
    }
}
