using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkdayCalendar.API.Dtos;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("This handles the workday calculation")]
    public class WorkdayController : ControllerBase
    {
        private readonly IWorkdayService _workdayService;

        public WorkdayController(IWorkdayService workdayService)
        {
            _workdayService = workdayService;
        }

        [HttpPost]
        [SwaggerOperation("Calculate workdays")]
        [SwaggerResponse(StatusCodes.Status200OK, "The OK response", typeof(DateTime))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The Internal Server Error response", typeof(string))]
        public async Task<IActionResult> AddWorkdaysAsync([FromBody] WorkdayRequestDto request)
        {
            var resultDate = await _workdayService.AddWorkdaysAsync(request.StartDate, request.Workdays);
            return Ok(resultDate);
        }
    }
}
