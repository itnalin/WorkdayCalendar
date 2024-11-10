namespace WorkdayCalendar.DomainLayer.Entities
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }        
    }
}
