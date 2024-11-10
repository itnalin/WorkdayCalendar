using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.InfrastructureLayer
{
    public class WorkdayCalendarContext : DbContext, IWorkdayCalendarContext
    {
        public DbSet<Holiday> Holidays { get; set; }        
        
        private readonly string _connectionString;

        public WorkdayCalendarContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("WorkdayCalendarDatabase")!;            
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Update(entity);
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite(_connectionString);

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }
    }
}
