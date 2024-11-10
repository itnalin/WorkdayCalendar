using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.ServiceLayer.Interfaces;
using WorkdayCalendar.ServiceLayer.Services;

namespace WorkdayCalendar.ServiceLayer.Tests.Services
{
    [TestClass()]
    public class HolidayServiceTests
    {   
        private IHolidayManagerService _holidayManagerService = null!;       
        private IHolidayService _holidayService = null!;

        [TestInitialize]
        public void TestInitialize()
        {                        
            _holidayManagerService = Substitute.For<IHolidayManagerService>();
            _holidayService = new HolidayService(_holidayManagerService);
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

            _holidayManagerService.GetAllHolidaysAsync().Returns(holidays);

            // Act
            var result = await _holidayService.GetAllHolidaysAsync();

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
        public async Task AddHolidayAsync_ReturnsHolidayEntity()
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


            _holidayManagerService.AddHolidayAsync(Arg.Any<Holiday>()).Returns(returnHoliday);

            // Act
            var result = await _holidayService.AddHolidayAsync(holiday);

            // Assert
            await _holidayManagerService.Received(1).AddHolidayAsync(Arg.Any<Holiday>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Holiday));
            Assert.AreEqual(returnHoliday.Id, result.Id);
            Assert.AreEqual(returnHoliday.Date, result.Date);
            Assert.AreEqual(returnHoliday.IsRecurring, result.IsRecurring);
        }

        [TestMethod]
        public async Task UpdateHolidayAsync_ReturnsHolidayEntity()
        {
            // Arrange
            var date = DateTime.Now;

            var holiday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };

            var returnHoliday = new Holiday()
            {
                Id = 1,
                Date = date,
                IsRecurring = true,
            };


            _holidayManagerService.UpdateHolidayAsync(Arg.Any<Holiday>()).Returns(returnHoliday);

            // Act
            var result = await _holidayService.UpdateHolidayAsync(holiday);

            // Assert
            await _holidayManagerService.Received(1).UpdateHolidayAsync(Arg.Any<Holiday>());
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Holiday));
            Assert.AreEqual(returnHoliday.Id, result.Id);
            Assert.AreEqual(returnHoliday.Date, result.Date);
            Assert.AreEqual(returnHoliday.IsRecurring, result.IsRecurring);
        }

        [TestMethod]
        public async Task DeleteHolidayAsync_ReturnsTaskCompleted()
        {
            // Arrange
            var id = 1;            

            _holidayManagerService.DeleteHolidayAsync(Arg.Is(id)).Returns(Task.CompletedTask);

            // Act
            await _holidayService.DeleteHolidayAsync(id);

            // Assert            
            await _holidayManagerService.Received(1).DeleteHolidayAsync(Arg.Is(id));
        }
    }
}
