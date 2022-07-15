using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeUserController : ControllerBase
    {
        private readonly ISubscribeUser _subscribeUser;

        public SubscribeUserController(ISubscribeUser subscribeUser)
        {
            _subscribeUser = subscribeUser;
        }

        [HttpGet]
        public async Task<IActionResult> SubscribeUser([FromQuery] WeatherStatisticalReportInputDataDto inputData)
        {
            return Ok(_subscribeUser.SubscribeUserByUserId(inputData));
        }
    }
}
