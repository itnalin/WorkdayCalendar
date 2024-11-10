using Microsoft.EntityFrameworkCore;
using NSubstitute;
using WorkdayCalendar.InfrastructureLayer.Tests.Utility;

namespace WorkdayCalendar.InfrastructureLayer.Tests.Helper
{
    public static class DbSetMockHelper
    {
        public static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>, IAsyncEnumerable<T>>();

            // Set up IQueryable
            ((IQueryable<T>)mockSet).Provider.Returns(queryable.Provider);
            ((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(queryable.ElementType);

            // Set up IAsyncEnumerable
            ((IAsyncEnumerable<T>)mockSet)
                .GetAsyncEnumerator(Arg.Any<CancellationToken>())
                .Returns(new AsyncEnumerator<T>(queryable.GetEnumerator()));

            return mockSet;
        }
    }
}
