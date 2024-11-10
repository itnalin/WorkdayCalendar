using Microsoft.Extensions.Options;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;

namespace WorkdayCalendar.DomainLayer.Services
{
    public class WorkdayCalculatorService : IWorkdayCalculatorService
    {        
        private readonly IHolidayManagerService _holidayManagerService;
        private readonly WorkdaySetting _settings;
                
        public WorkdayCalculatorService(IHolidayManagerService holidayManagerService, IOptions<WorkdaySetting> options)
        {
            _holidayManagerService = holidayManagerService;
            _settings = options.Value;
        }

        public async Task<DateTime> AddWorkdaysAsync(DateTime startDate, double workdays)
        {
            // If starting time is outside of working hours, adjust it to the previous/next working hour
            DateTime currentDateTime = AdjustToWorkHours(startDate);

            // Determine the direction (+ or -)
            bool isForward = workdays >= 0;
            workdays = Math.Abs(workdays);

            // Whole workdays to add/subtract
            int wholeWorkdays = (int)workdays;
            double fractionalWorkday = workdays - wholeWorkdays;

            // Move full workdays
            for (int i = 0; i < wholeWorkdays; i++)
            {
                currentDateTime = isForward ? await NextWorkday(currentDateTime) : await PreviousWorkday(currentDateTime);
            }

            // Adjust for the fractional workday
            TimeSpan fractionalTime = TimeSpan.FromHours(fractionalWorkday * _settings.WorkdayDuration.TotalHours);
            currentDateTime = isForward ? await AddFractionalWorkday(currentDateTime, fractionalTime) : await SubtractFractionalWorkday(currentDateTime, fractionalTime);

            return currentDateTime;
        }

        #region private methods

        // Adjusts a datetime to be within working hours if it falls outside
        private DateTime AdjustToWorkHours(DateTime dateTime)
        {
            if (dateTime.TimeOfDay < _settings.WorkdayStart)
                return dateTime.Date + _settings.WorkdayStart;
            if (dateTime.TimeOfDay > _settings.WorkdayEnd)
                return dateTime.Date + _settings.WorkdayEnd;
            return dateTime;
        }

        // Move to the next workday at the starting work hour
        private async Task<DateTime> NextWorkday(DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            } while (IsWeekend(date) || await _holidayManagerService.IsHolidayAsync(date));
            return date.Date + _settings.WorkdayStart;
        }

        // Move to the previous workday at the ending work hour
        private async Task<DateTime> PreviousWorkday(DateTime date)
        {
            do
            {
                date = date.AddDays(-1);
            } while (IsWeekend(date) || await _holidayManagerService.IsHolidayAsync(date));
            return date.Date + _settings.WorkdayEnd;
        }

        // Add fractional hours to a date within work hours
        private async Task<DateTime> AddFractionalWorkday(DateTime dateTime, TimeSpan fractionalTime)
        {
            TimeSpan remainingWorkHours = _settings.WorkdayEnd - dateTime.TimeOfDay;

            if (fractionalTime <= remainingWorkHours)
                return dateTime + fractionalTime;
            else
                return await NextWorkday(dateTime) + (fractionalTime - remainingWorkHours);
        }

        // Subtract fractional hours from a date within work hours
        private async Task<DateTime> SubtractFractionalWorkday(DateTime dateTime, TimeSpan fractionalTime)
        {
            TimeSpan timeSpent = dateTime.TimeOfDay - _settings.WorkdayStart;

            if (fractionalTime <= timeSpent)
                return dateTime - fractionalTime;
            else
                return await PreviousWorkday(dateTime) - (fractionalTime - timeSpent);
        }

        // Check if a date is on a weekend
        private static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        #endregion
    }
}
