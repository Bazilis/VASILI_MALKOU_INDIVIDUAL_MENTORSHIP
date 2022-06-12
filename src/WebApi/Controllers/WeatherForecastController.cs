using BLL.Dto;
using BLL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWeatherForecast _weatherForecastService;
        private readonly IValidator<WeatherForecastInputDataDto> _weatherForecastValidator;

        public WeatherForecastController(IConfiguration configuration, IWeatherForecast weatherForecastService, IValidator<WeatherForecastInputDataDto> weatherForecastValidator)
        {
            _configuration = configuration;
            _weatherForecastService = weatherForecastService;
            _weatherForecastValidator = weatherForecastValidator;
        }

        [HttpGet]
        public IActionResult Get(string cityname, int numberOfDays)
        {
            var inputData = new WeatherForecastInputDataDto
            {
                CityName = cityname,
                NumberOfDays = numberOfDays,
                MinNumberDays = Convert.ToInt32(_configuration["MinNumberDays"]),
                MaxNumberDays = Convert.ToInt32(_configuration["MaxNumberDays"])
            };

            var validationResult = _weatherForecastValidator.Validate(inputData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok(_weatherForecastService.GetWeatherForecast(inputData));
        }
    }
}
