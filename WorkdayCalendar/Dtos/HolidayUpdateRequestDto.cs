using Microsoft.AspNetCore.Mvc;

namespace WorkdayCalendar.API.Dtos
{
    public class HolidayUpdateRequestDto
    {
        [FromRoute(Name = "id")]
        public required int Id { get; init; }
        [FromBody]
        public required HolidayRequestDto holiday { get; init; }
    }
}
