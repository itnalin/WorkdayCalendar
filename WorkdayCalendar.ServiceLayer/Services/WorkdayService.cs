using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.ServiceLayer.Services
{
    public class WorkdayService : IWorkdayService
    {
        private readonly IWorkdayCalculatorService _workdayCalculatorService;
        
        public WorkdayService(IWorkdayCalculatorService workdayCalculatorService)
        {
            _workdayCalculatorService = workdayCalculatorService;            
        }

        public async Task<DateTime> AddWorkdaysAsync(DateTime startDate, double workdays)
        {
            return await _workdayCalculatorService.AddWorkdaysAsync(startDate, workdays);
        }        
    }
}
