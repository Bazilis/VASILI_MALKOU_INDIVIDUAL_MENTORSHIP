using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeUserController : ControllerBase
    {
        private readonly IWeatherStatisticalReport _weatherStatisticalReport;

        public SubscribeUserController(IWeatherStatisticalReport weatherStatisticalReport)
        {
            _weatherStatisticalReport = weatherStatisticalReport;
        }

        [HttpGet]
        public IActionResult SubscribeUser([FromQuery] WeatherStatisticalReportInputDataDto inputData)
        {
            return Ok(_weatherStatisticalReport.GetWeatherStatisticalReport(inputData.CitiesString, inputData.TimePeriod));
        }
    }
}
