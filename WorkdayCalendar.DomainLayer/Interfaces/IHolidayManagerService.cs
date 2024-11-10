using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.DomainLayer.Interfaces
{
    public interface IHolidayManagerService
    {
        Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
        Task<Holiday> AddHolidayAsync(Holiday holiday);
        Task<Holiday> UpdateHolidayAsync(Holiday holiday);
        Task DeleteHolidayAsync(int id);
        Task<bool> IsHolidayAsync(DateTime date);
    }
}
