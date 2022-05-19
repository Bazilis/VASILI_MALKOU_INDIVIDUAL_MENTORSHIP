﻿using BLL.Dto;
using BLL.Services;
using Xunit;

namespace IntegrationTests
{
    public class ForecastServiceIntegrationTests
    {
        [Fact]
        public void GetForecastMethodReturnsValidResult()
        {
            // arrange
            string expectedRegexPattern = @"In Tashkent(\w*)";

            var inputData = new InputDataDto { CityName = "Tashkent" };

            var service = new ForecastService();

            // act
            string result = service.GetForecast(inputData);

            // assert
            Assert.Matches(expectedRegexPattern, result);
        }
    }
}
