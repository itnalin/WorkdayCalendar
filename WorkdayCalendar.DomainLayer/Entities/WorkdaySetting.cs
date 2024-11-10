namespace WorkdayCalendar.DomainLayer.Entities
{
    public class WorkdaySetting
    {       
        public TimeSpan WorkdayStart { get; set; }
        public TimeSpan WorkdayEnd { get; set; }

        public TimeSpan WorkdayDuration => WorkdayEnd - WorkdayStart;
    }
}
