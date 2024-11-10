namespace WorkdayCalendar.API.Dtos
{
    public class WorkdayRequestDto
    {
        public required DateTime StartDate { get; set; }
        public required double Workdays { get; set; }
    }
}
