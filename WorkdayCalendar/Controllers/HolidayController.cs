using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using WorkdayCalendar.API.Dtos;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("This handles the CRUD operations for holiday")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;

        public HolidayController(IHolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get all holidays")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The Internal Server Error response", typeof(string))]
        public async Task<ActionResult<IEnumerable<HolidayResponseDto>>> GetAllHolidaysAsync()
        {
            var result = await _holidayService.GetAllHolidaysAsync();
            var response = _mapper.Map<IEnumerable<HolidayResponseDto>>(result);

            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation("Add a holiday")]
        [Produces(MediaTypeNames.Application.Json)]
        [SwaggerResponse(StatusCodes.Status201Created, "The Created response", typeof(HolidayResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The BadRequest response", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The Internal Server Error response", typeof(string))]
        public async Task<ActionResult<HolidayResponseDto>> AddHolidayAsync([FromBody] HolidayRequestDto request)
        {
            var holiday = _mapper.Map<Holiday>(request);
            var result = await _holidayService.AddHolidayAsync(holiday);
            var response = _mapper.Map<HolidayResponseDto>(result);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id:int}")]
        [SwaggerOperation("Update a holiday")]
        [SwaggerResponse(StatusCodes.Status200OK, "The OK response", typeof(HolidayResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound response", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The Internal Server Error response", typeof(string))]
        public async Task<ActionResult<HolidayResponseDto>> UpdateHolidayAsync(HolidayUpdateRequestDto request)
        {
            var holiday = _mapper.Map<Holiday>(request);
            var result = await _holidayService.UpdateHolidayAsync(holiday);
            var response = _mapper.Map<HolidayResponseDto>(result);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The NoContent response", typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound response", typeof(string))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "The Internal Server Error response", typeof(string))]
        public async Task<IActionResult> DeleteHolidayAsync(int id)
        {
            await _holidayService.DeleteHolidayAsync(id);

            return NoContent();
        }
    }
}
