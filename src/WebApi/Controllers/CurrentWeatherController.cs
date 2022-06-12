using BLL.Dto;
using BLL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentWeatherController : ControllerBase
    {
        private readonly ICurrentWeather _currentWeatherService;
        private readonly IValidator<CurrentWeatherInputDataDto> _currentWeatherValidator;

        public CurrentWeatherController(ICurrentWeather currentWeatherService, IValidator<CurrentWeatherInputDataDto> currentWeatherValidator)
        {
            _currentWeatherService = currentWeatherService;
            _currentWeatherValidator = currentWeatherValidator;
        }

        [HttpGet]
        public IActionResult Get(string cityname)
        {
            var inputData = new CurrentWeatherInputDataDto { CityName = cityname };

            var validationResult = _currentWeatherValidator.Validate(inputData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok(_currentWeatherService.GetCurrentWeather(inputData));
        }
    }
}
