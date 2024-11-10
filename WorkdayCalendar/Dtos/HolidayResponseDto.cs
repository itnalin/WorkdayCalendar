namespace WorkdayCalendar.API.Dtos
{
    public class HolidayResponseDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}
