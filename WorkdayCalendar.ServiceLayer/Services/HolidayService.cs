using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.ServiceLayer.Services
{
    public class HolidayService : IHolidayService
    {        
        private readonly IHolidayManagerService _holidayManagerService;

        public HolidayService(IHolidayManagerService holidayManagerService)
        {   
            _holidayManagerService = holidayManagerService;
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
        {
            return await _holidayManagerService.GetAllHolidaysAsync();
        }

        public async Task<Holiday> AddHolidayAsync(Holiday holiday)
        {
            return await _holidayManagerService.AddHolidayAsync(holiday);
        }

        public async Task<Holiday> UpdateHolidayAsync(Holiday holiday)
        {
            return await _holidayManagerService.UpdateHolidayAsync(holiday);
        }

        public async Task DeleteHolidayAsync(int id)
        {
            await _holidayManagerService.DeleteHolidayAsync(id);
        }
    }
}
