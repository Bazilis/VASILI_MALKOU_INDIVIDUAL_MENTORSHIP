using BLL.Dto;
using BLL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherHistoryReaderController : ControllerBase
    {
        private readonly IWeatherHistoryReader _weatherHistoryReaderService;
        private readonly IValidator<WeatherHistoryReaderInputDataDto> _weatherHistoryReaderInputDataValidator;

        public WeatherHistoryReaderController(IWeatherHistoryReader weatherHistoryReaderService, IValidator<WeatherHistoryReaderInputDataDto> weatherHistoryReaderInputDataValidator)
        {
            _weatherHistoryReaderService = weatherHistoryReaderService;
            _weatherHistoryReaderInputDataValidator = weatherHistoryReaderInputDataValidator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]WeatherHistoryReaderInputDataDto inputData)
        {
            var validationResult = _weatherHistoryReaderInputDataValidator.Validate(inputData);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            return Ok(_weatherHistoryReaderService.GetWeatherHistoryData(inputData));
        }
    }
}
