using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.DomainLayer.Services;
using WorkdayCalendar.InfrastructureLayer;
using WorkdayCalendar.InfrastructureLayer.Repositories;
using WorkdayCalendar.ServiceLayer.Interfaces;
using WorkdayCalendar.ServiceLayer.Services;

namespace WorkdayCalendar.ServiceLayer.Startup
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddSingleton<WorkdaySetting>(sp => sp.GetRequiredService<IOptions<WorkdaySetting>>().Value);

            services.AddScoped<IWorkdayCalculatorService, WorkdayCalculatorService>();
            services.AddScoped<IHolidayManagerService, HolidayManagerService>();

            services.AddScoped<IHolidayRepository, HolidayRepository>();

            services.AddScoped<IWorkdayService, WorkdayService>();
            services.AddScoped<IHolidayService, HolidayService>();
            
            services.AddScoped<IWorkdayCalendarContext, WorkdayCalendarContext>();
        }
    }
}
