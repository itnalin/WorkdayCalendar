using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Exceptions;
using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.DomainLayer.Services;

namespace WorkdayCalendar.DomainLayer.Tests.Services
{
    [TestClass()]
    public class HolidayManagerServiceTests
    {
        private IHolidayRepository _mockHolidayRepository = null!;
        private IHolidayManagerService _holidayManagerService = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHolidayRepository = Substitute.For<IHolidayRepository>();            
            _holidayManagerService = new HolidayManagerService(_mockHolidayRepository);
        }

        [TestMethod]
        public async Task GetAllHolidaysAsync_ReturnsHolidayEntities()
        {
            // Arrange
            var holidays = new List<Holiday>
            {
                new Holiday { Id = 1, Date = new DateTime(), IsRecurring = true },
                new Holiday { Id = 2, Date = new DateTime(), IsRecurring = true },
                new Holiday { Id = 3, Date = new DateTime(), IsRecurring = false }

            };
                        
            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _holidayManagerService.GetAllHolidaysAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Holiday>));

            for (int i = 0; i < holidays!.Count(); i++)
            {
                Assert.AreEqual(holidays!.ElementAt(i).Id, result.ToList()[i].Id);
                Assert.AreEqual(holidays!.ElementAt(i).Date, result.ToList()[i].Date);
                Assert.AreEqual(holidays!.ElementAt(i).IsRecurring, result.ToList()[i].IsRecurring);
            }
        }

        [TestMethod]
        public void AddHolidayAsync_ThrowsInvalidInputParameterException_WhenHolidayEntityIsNull()
        {
            // Arrange
            var date = DateTime.Now;

            Holiday holiday = null!;

            // Act
            var ex = Assert.ThrowsException<InvalidInputParameterException>(() => _holidayManagerService.AddHolidayAsync(holiday).GetAwaiter().GetResult());

            // Assert
            Assert.AreEqual("Invalid holiday entity", ex.Message);
        }

        [TestMethod]
        public async Task AddHolidayAsync_AddAndReturnsHolidayEntity()
        {
            // Arrange
            var date = DateTime.Now;

            var holiday = new Holiday
            {
                Date = date,
                IsRecurring = true,
            };

            var returnHoliday = new Holiday()
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };

            
            _mockHolidayRepository.AddAsync(Arg.Any<Holiday>()).Returns(returnHoliday);
            
            // Act
            var result = await _holidayManagerService.AddHolidayAsync(holiday);

            // Assert
            await _mockHolidayRepository.Received(1).AddAsync(Arg.Any<Holiday>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Holiday));
            Assert.AreEqual(returnHoliday.Id, result.Id);
            Assert.AreEqual(returnHoliday.Date, result.Date);
            Assert.AreEqual(returnHoliday.IsRecurring, result.IsRecurring);
        }

        [TestMethod]
        public void UpdateHolidayAsync_ThrowsNotFoundException_WhenIdIsNotExisting()
        {
            // Arrange
            var date = DateTime.Now;

            var holiday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };

            _mockHolidayRepository.GetByIdAsync(Arg.Is(holiday.Id)).ReturnsNull();

            // Act
            var ex = Assert.ThrowsException<NotFoundException>(() => _holidayManagerService.UpdateHolidayAsync(holiday).GetAwaiter().GetResult());

            // Assert
            Assert.AreEqual("Holiday id not found", ex.Message);
        }

        [TestMethod]
        public async Task UpdateHolidayAsync_UpdateAndReturnsHolidayEntity()
        {
            // Arrange
            var date = DateTime.Now;

            var holiday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };

            var existingHoliday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = false,
            };

            var returnHoliday = new Holiday()
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };

            _mockHolidayRepository.GetByIdAsync(Arg.Is(holiday.Id)).Returns(existingHoliday);
            _mockHolidayRepository.UpdateAsync(Arg.Any<Holiday>()).Returns(returnHoliday);

            // Act
            var result = await _holidayManagerService.UpdateHolidayAsync(holiday);

            // Assert
            await _mockHolidayRepository.Received(1).UpdateAsync(Arg.Any<Holiday>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Holiday));
            Assert.AreEqual(returnHoliday.Id, result.Id);
            Assert.AreEqual(returnHoliday.Date, result.Date);
            Assert.AreEqual(returnHoliday.IsRecurring, result.IsRecurring);
        }

        [TestMethod]
        public void DeleteHolidayAsync_ThrowsNotFoundException_WhenIdIsNotExisting()
        {
            // Arrange
            var id = 1;

            _mockHolidayRepository.GetByIdAsync(Arg.Is(id)).ReturnsNull();

            // Act
            var ex = Assert.ThrowsException<NotFoundException>(() => _holidayManagerService.DeleteHolidayAsync(1).GetAwaiter().GetResult());

            // Assert
            Assert.AreEqual("Holiday id not found", ex.Message);
        }

        [TestMethod]
        public async Task DeleteHolidayAsync_ReturnsTaskCompleted()
        {
            // Arrange
            var id = 1;

            var holiday = new Holiday
            {
                Id = id,
                Date = DateTime.Now,
                IsRecurring = true,
            };

            _mockHolidayRepository.GetByIdAsync(id).Returns(holiday);
            _mockHolidayRepository.DeleteAsync(Arg.Is(holiday)).Returns(Task.CompletedTask);

            // Act
            await _holidayManagerService.DeleteHolidayAsync(id);

            // Assert            
            await _mockHolidayRepository.Received(1).DeleteAsync(Arg.Is(holiday));
        }

        [TestMethod]
        public async Task IsHolidayAsync_ReturnsTrue_WhenPassingDateIsHoliday()
        {
            // Arrange
            var date = new DateTime(2004, 05, 24, 18, 05, 0);

            var holidays = new List<Holiday>
            {
                new Holiday { Id = 1, Date = new DateTime(2004, 05, 24, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 2, Date = new DateTime(2004, 05, 17, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 3, Date = new DateTime(2004, 05, 16, 18, 05, 0), IsRecurring = false }

            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _holidayManagerService.IsHolidayAsync(date);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsHolidayAsync_ReturnsFalse_WhenPassingDateIsNotHoliday()
        {
            // Arrange
            var date = new DateTime(2004, 05, 24, 18, 05, 0);

            var holidays = new List<Holiday>
            {
                new Holiday { Id = 1, Date = new DateTime(2004, 05, 25, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 2, Date = new DateTime(2004, 05, 17, 18, 05, 0), IsRecurring = true },
                new Holiday { Id = 3, Date = new DateTime(2004, 05, 16, 18, 05, 0), IsRecurring = false }

            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _holidayManagerService.IsHolidayAsync(date);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse(result);
        }
    }
}
