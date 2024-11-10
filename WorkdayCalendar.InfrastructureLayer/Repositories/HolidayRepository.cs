using Microsoft.EntityFrameworkCore;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;

namespace WorkdayCalendar.InfrastructureLayer.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {   
        private readonly IWorkdayCalendarContext _dbContext;
        public HolidayRepository(IWorkdayCalendarContext dbContext) 
        {
            _dbContext = dbContext;        
        }

        public async Task<Holiday?> GetByIdAsync(int id)
        {
            return await _dbContext.Holidays.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Holiday>> GetAllAsync()
        {
            return await _dbContext.Holidays.ToListAsync();
        }

        public async Task<Holiday> AddAsync(Holiday entity)
        {
            _dbContext.Holidays.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<Holiday> UpdateAsync(Holiday entity)
        {
            _dbContext.UpdateEntity(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Holiday entity)
        {
            _dbContext.RemoveEntity(entity);
            await _dbContext.SaveChangesAsync();
        }              
    }
}