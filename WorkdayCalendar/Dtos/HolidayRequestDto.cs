namespace WorkdayCalendar.API.Dtos
{
    public class HolidayRequestDto
    {
        public required DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
