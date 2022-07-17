using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace WebApi.Controllers
{
    //[Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISubscribeUser _subscribeUser;

        public SubscribeUserController(IConfiguration configuration, ISubscribeUser subscribeUser)
        {
            _configuration = configuration;
            _subscribeUser = subscribeUser;
        }

        [HttpGet]
        public IActionResult SubscribeUser([FromQuery] WeatherStatisticalReportInputDataDto inputData)
        {
            var isUseRabbitmq = Convert.ToBoolean(_configuration["IsUseRabbitmq"]);

            return Ok(_subscribeUser.SubscribeUserByUserId(inputData, isUseRabbitmq));
        }
    }
}
