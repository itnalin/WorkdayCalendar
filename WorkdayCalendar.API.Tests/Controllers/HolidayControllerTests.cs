using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkdayCalendar.API.Controllers;
using WorkdayCalendar.API.Dtos;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.API.Tests.Controllers
{
    [TestClass]
    public class HolidayControllerTests
    {
        private HolidayController _controller = null!;
        private IHolidayService _mockHolidayService = null!;
        private static IMapper _mapper = null!;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHolidayService = Substitute.For<IHolidayService>();

            _controller = new HolidayController(_mockHolidayService, _mapper);
        }

        [TestMethod]
        public async Task GetAllHolidaysAsync_ReturnsHolidayResponseDtos()
        {
            // Arrange
            var holidays = new List<Holiday>
            {
                new Holiday { Id = 1, Date = new DateTime(), IsRecurring = true },
                new Holiday { Id = 2, Date = new DateTime(), IsRecurring = true },
                new Holiday { Id = 3, Date = new DateTime(), IsRecurring = false }

            };
            
            _mockHolidayService.GetAllHolidaysAsync().Returns(holidays);

            // Act
            var result = await _controller.GetAllHolidaysAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<HolidayResponseDto>>));

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var holidayResponseDtos = (result.Result as OkObjectResult)!.Value as IEnumerable<HolidayResponseDto>;
            Assert.AreEqual(holidayResponseDtos!.Count(), holidays.Count);

            for (int i = 0; i < holidayResponseDtos!.Count(); i++)
            {
                Assert.AreEqual(holidayResponseDtos!.ElementAt(i).Id, holidays[i].Id);
                Assert.AreEqual(holidayResponseDtos!.ElementAt(i).Date, holidays[i].Date);
                Assert.AreEqual(holidayResponseDtos!.ElementAt(i).IsRecurring, holidays[i].IsRecurring);
            }
        }

        [TestMethod]
        public async Task AddHolidayAsync_ReturnsHolidayResponseDto()
        {
            // Arrange      
            var date = new DateTime();

            var holiday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true
            };            

            var holidayRequestDto = new HolidayRequestDto
            {
                Date = date,
                IsRecurring = true
            };

            var holidayResponseDto = new HolidayResponseDto
            {
                Id = 1,
                Date = date,
                IsRecurring = true
            };

            _mockHolidayService.AddHolidayAsync(Arg.Any<Holiday>()).Returns(holiday);

            // Act
            var result = await _controller.AddHolidayAsync(holidayRequestDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedResult));

            var createdResult = result.Result as CreatedResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);

            var holidayResponseDtoObject = (result.Result as CreatedResult)!.Value as HolidayResponseDto;
            Assert.AreEqual(holidayResponseDto!.Id, holidayResponseDtoObject!.Id);
            Assert.AreEqual(holidayResponseDto!.Date, holidayResponseDtoObject.Date);
            Assert.AreEqual(holidayResponseDto!.IsRecurring, holidayResponseDtoObject.IsRecurring);
        }

        [TestMethod]
        public async Task UpdateHolidayAsync_ReturnsHolidayResponseDto()
        {
            // Arrange
            var date = new DateTime();

            var holiday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true
            };

            var holidayUpdateRequestDto = new HolidayUpdateRequestDto
            {
                Id = 1,
                holiday = new HolidayRequestDto
                {
                    Date = date,
                    IsRecurring = true
                }
            };

            var returnHoliday = new Holiday
            {
                Id = 1,
                Date = date,
                IsRecurring = true
            };

            _mockHolidayService.UpdateHolidayAsync(Arg.Any<Holiday>()).Returns(holiday);

            // Act
            var result = await _controller.UpdateHolidayAsync(holidayUpdateRequestDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<HolidayResponseDto>));

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var holidayResponseDtoObject = (result.Result as OkObjectResult)!.Value as HolidayResponseDto;
            Assert.AreEqual(returnHoliday.Id, holidayResponseDtoObject!.Id); 
            Assert.AreEqual(returnHoliday.Date, holidayResponseDtoObject!.Date);
            Assert.AreEqual(returnHoliday.IsRecurring, holidayResponseDtoObject!.IsRecurring);
        }

        [TestMethod]
        public async Task DeleteHolidayAsync_ReturnsNoContent()
        {
            // Arrange
            var holidayId = 1;
            _mockHolidayService.DeleteHolidayAsync(holidayId).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteHolidayAsync(holidayId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult!.StatusCode);
        }
    }
}
