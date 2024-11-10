namespace WorkdayCalendar.ServiceLayer.Interfaces
{
    public interface IWorkdayService
    {
        Task<DateTime> AddWorkdaysAsync(DateTime startDate, double workdays);
    }
}
