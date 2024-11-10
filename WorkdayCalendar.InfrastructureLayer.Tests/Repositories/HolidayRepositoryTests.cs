using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.InfrastructureLayer.Repositories;
using WorkdayCalendar.InfrastructureLayer.Tests.Helper;

namespace WorkdayCalendar.InfrastructureLayer.Tests.Repositories
{
    [TestClass()]
    public class HolidayRepositoryTests
    {   
        private IWorkdayCalendarContext _dbContext = null!;
        private HolidayRepository _holidayRepository = null!;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = Substitute.For<IWorkdayCalendarContext>();  
            _holidayRepository = new HolidayRepository(_dbContext);
        }        

        [TestMethod()]
        public async Task GetAllAsync_ReturnHolidayEntityList()
        {
            // Arrange
            var holidays = new List<Holiday>
            {
                new Holiday { Id = 1, Date = new DateTime(2004, 05, 24, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 2, Date = new DateTime(2004, 05, 17, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 3, Date = new DateTime(2004, 05, 16, 18, 05, 0), IsRecurring = false }
            };           

            var mockSet = DbSetMockHelper.MockDbSet<Holiday>(holidays);
            _dbContext.Holidays.Returns(mockSet);

            // Act
            var result = await _holidayRepository.GetAllAsync();

            // Assert            
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllAsync_ReturnHolidayEntityEmptyList_WhenNoHolidayExisting()
        {
            // Arrange
            var holidays = new List<Holiday>();
     
            var mockSet = DbSetMockHelper.MockDbSet<Holiday>(holidays);
            _dbContext.Holidays.Returns(mockSet);

            // Act
            var result = await _holidayRepository.GetAllAsync();

            // Assert            
            Assert.AreEqual(0, result.Count());
        }       

        [TestMethod()]
        public async Task AddAsync_ShouldAddEntityAndSaveChanges()
        {
            // Arrange            
            var mockSet = Substitute.For<DbSet<Holiday>>();

            var newHoliday = new Holiday { Date = new DateTime(2023, 12, 25), IsRecurring = false };
            
            _dbContext.Holidays.Returns(mockSet);

            // Act
            var result = await _holidayRepository.AddAsync(newHoliday);

            // Assert            
            await _dbContext.Received(1).SaveChangesAsync();
            Assert.AreEqual(newHoliday, result);
        }

        [TestMethod()]
        public async Task UpdateAsync_ShouldUpdateEntityAndSaveChanges()
        {
            // Arrange            
            var holidayToUpdate = new Holiday { Id = 1, Date = new DateTime(2023, 12, 25), IsRecurring = false };

            var mockSet = Substitute.For<DbSet<Holiday>>();
            _dbContext.Holidays.Returns(mockSet);

            // Act
            var result = await _holidayRepository.UpdateAsync(holidayToUpdate);

            // Assert            
            await _dbContext.Received(1).SaveChangesAsync();
            Assert.AreEqual(holidayToUpdate, result);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveEntityAndSaveChanges()
        {
            // Arrange            
            var holidayToDelete = new Holiday { Id = 1, Date = new DateTime(2023, 12, 25), IsRecurring = false };

            // Act
            await _holidayRepository.DeleteAsync(holidayToDelete);

            // Assert            
            _dbContext.Received(1).RemoveEntity(holidayToDelete);          
            await _dbContext.Received(1).SaveChangesAsync();
        }
    }   
}
