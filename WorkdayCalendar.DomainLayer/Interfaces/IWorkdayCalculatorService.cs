namespace WorkdayCalendar.DomainLayer.Interfaces
{
    public interface IWorkdayCalculatorService
    {
        Task<DateTime> AddWorkdaysAsync(DateTime startDate, double workdays);
    }
}
