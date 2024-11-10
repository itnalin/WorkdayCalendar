using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.DomainLayer.Interfaces;
using WorkdayCalendar.DomainLayer.Services;

namespace WorkdayCalendar.DomainLayer.Tests.Services
{
    [TestClass()]
    public class WorkdayCalculatorServiceTests
    {
        private IHolidayRepository _mockHolidayRepository = null!;
        private IHolidayManagerService _holidayManagerService = null!;  
        private IWorkdayCalculatorService _workdayCalculatorService = null!;
        private IOptions<WorkdaySetting> _options = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            var workdaySetting = new WorkdaySetting
            {
                WorkdayStart = new TimeSpan(8, 0, 0),
                WorkdayEnd = new TimeSpan(16, 0, 0)
            };

            _options = Substitute.For<IOptions<WorkdaySetting>>();
            _options.Value.Returns(workdaySetting);

            _mockHolidayRepository = Substitute.For<IHolidayRepository>();
            _holidayManagerService = new HolidayManagerService(_mockHolidayRepository);
            _workdayCalculatorService = new WorkdayCalculatorService(_holidayManagerService, _options);
        }

        [TestMethod]
        // 24-05-2004 18:05 with the addition of -5.5 working days is 14-05-2004 12:00
        public async Task AddWorkdaysAsync_TestCase1_ReturnsEndupWorkdate()
        {
            // Arrange
            var startDate = new DateTime(2004, 05, 24, 18, 05, 0);
            double workdays = -5.5;
            var endupWorkDate = new DateTime(2004, 05, 14, 12, 00, 0);

            var holidays = new List<Holiday>
            {
                new Holiday
                {
                    Id = 1,
                    Date = new DateTime(2004, 05, 17, 12, 00, 0),
                    IsRecurring = true
                },
                new Holiday
                {
                    Id = 2,
                    Date = new DateTime(2004, 05, 27, 12, 00, 0),
                    IsRecurring = false
                }
            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);            

            // Act
            var result = await _workdayCalculatorService.AddWorkdaysAsync(startDate, workdays);

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DateTime));
            Assert.AreEqual(endupWorkDate, result);
        }

        [TestMethod]
        // 24-05-2004 18:03 with the addition of  -6.7470217 working days is 13-05-2004 10:02
        public async Task AddWorkdaysAsync_TestCase2_ReturnsEndupWorkdate()
        {
            // Arrange
            var startDate = new DateTime(2004, 05, 24, 18, 03, 0);
            double workdays = -6.7470217;
            var endupWorkDate = new DateTime(2004, 05, 13, 10, 01, 0);

            var holidays = new List<Holiday>
            {
                new Holiday
                {
                    Id = 1,
                    Date = new DateTime(2004, 05, 17, 12, 00, 0),
                    IsRecurring = true
                },
                new Holiday
                {
                    Id = 2,
                    Date = new DateTime(2004, 05, 27, 12, 00, 0),
                    IsRecurring = false
                }
            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _workdayCalculatorService.AddWorkdaysAsync(startDate, workdays);

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DateTime));
            Assert.AreEqual(endupWorkDate.Date, result.Date);
        }

        [TestMethod]
        // 24-05-2004 08:03 with the addition of 12.782709 working days is 10-06-2004 14:18
        public async Task AddWorkdaysAsync_TestCase3_ReturnsEndupWorkdate()
        {
            // Arrange
            var startDate = new DateTime(2004, 05, 24, 08, 03, 0);
            double workdays = 12.782709;
            var endupWorkDate = new DateTime(2004, 06, 10, 10, 01, 0);

            var holidays = new List<Holiday>
            {
                new Holiday
                {
                    Id = 1,
                    Date = new DateTime(2004, 05, 17, 12, 00, 0),
                    IsRecurring = true
                },
                new Holiday
                {
                    Id = 2,
                    Date = new DateTime(2004, 05, 27, 12, 00, 0),
                    IsRecurring = false
                }
            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _workdayCalculatorService.AddWorkdaysAsync(startDate, workdays);

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DateTime));
            Assert.AreEqual(endupWorkDate.Date, result.Date);
        }

        [TestMethod]
        // 24-05-2004 07:03 with the addition of 8.276628 working days is 04-06-2004 10:12
        public async Task AddWorkdaysAsync_TestCase4_ReturnsEndupWorkdate()
        {
            // Arrange
            var startDate = new DateTime(2004, 05, 24, 07, 03, 0);
            double workdays = 8.276628;
            var endupWorkDate = new DateTime(2004, 06, 04, 10, 12, 0);

            var holidays = new List<Holiday>
            {
                new Holiday
                {
                    Id = 1,
                    Date = new DateTime(2004, 05, 17, 12, 00, 0),
                    IsRecurring = true
                },
                new Holiday
                {
                    Id = 2,
                    Date = new DateTime(2004, 05, 27, 12, 00, 0),
                    IsRecurring = false
                }
            };

            _mockHolidayRepository.GetAllAsync().Returns(holidays);

            // Act
            var result = await _workdayCalculatorService.AddWorkdaysAsync(startDate, workdays);

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DateTime));
            Assert.AreEqual(endupWorkDate.Date, result.Date);
        }
    }
}
