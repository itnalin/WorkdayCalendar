using Microsoft.EntityFrameworkCore;
using WorkdayCalendar.DomainLayer.Entities;

namespace WorkdayCalendar.InfrastructureLayer
{
    public interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }

    public interface IWorkdayCalendarContext
    {
        DbSet<Holiday> Holidays { get; set; }       

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void UpdateEntity<TEntity>(TEntity entity) where TEntity : class;
        void RemoveEntity<TEntity>(TEntity entity) where TEntity : class;

    }
}
