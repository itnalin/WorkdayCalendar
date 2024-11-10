using Microsoft.EntityFrameworkCore;
using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.InfrastructureLayer
{
    public class WorkdayCalendarContext : DbContext, IWorkdayCalendarContext
    {
        public DbSet<Holiday> Holidays { get; set; }        

        public string DbPath { get; }

        public WorkdayCalendarContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "WorkdayCalendar.db");
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
        => options.UseSqlite($"Data Source={DbPath}");

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }
    }
}
