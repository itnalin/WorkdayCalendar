using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.ServiceLayer.Interfaces
{
    public interface IHolidayService
    {
        Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
        Task<Holiday> AddHolidayAsync(Holiday holiday);
        Task<Holiday> UpdateHolidayAsync(Holiday holiday);
        Task DeleteHolidayAsync(int id);
    }
}
