using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Exceptions;
using WorkdayCalendar.DomainLayer.Interfaces;

namespace WorkdayCalendar.DomainLayer.Services
{
    public class HolidayManagerService : IHolidayManagerService
    {
        private readonly IHolidayRepository _holidayRepository;
        public HolidayManagerService(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
        {
            return await _holidayRepository.GetAllAsync();
        }

        public async Task<Holiday> AddHolidayAsync(Holiday holiday)
        {
            if (holiday is null)
                throw new InvalidInputParameterException("Invalid holiday entity");

            return await _holidayRepository.AddAsync(holiday);
        }

        public async Task<Holiday> UpdateHolidayAsync(Holiday holiday)
        {
            var existingHoliday = await _holidayRepository.GetByIdAsync(holiday.Id);

            if (existingHoliday is null)
                throw new NotFoundException("Holiday id not found");

            return await _holidayRepository.UpdateAsync(holiday);
        }

        public async Task DeleteHolidayAsync(int id)
        {
            var holiday = await _holidayRepository.GetByIdAsync(id);

            if (holiday is null)
                throw new NotFoundException("Holiday id not found");

            await _holidayRepository.DeleteAsync(holiday);
        }

        public async Task<bool> IsHolidayAsync(DateTime date)
        {
            var holidays = await _holidayRepository.GetAllAsync();

            return holidays.Any(h => h.IsRecurring 
                ? h.Date.Month == date.Month && h.Date.Day == date.Day 
                : h.Date.Date == date.Date);
        }
    }
}
