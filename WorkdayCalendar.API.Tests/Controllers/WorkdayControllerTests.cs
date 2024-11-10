using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using WorkdayCalendar.API.Controllers;
using WorkdayCalendar.API.Dtos;
using WorkdayCalendar.ServiceLayer.Interfaces;

namespace WorkdayCalendar.API.Tests.Controllers
{
    [TestClass]
    public class WorkdayControllerTests
    {
        private WorkdayController _controller = null!;
        private IWorkdayService _mockWorkdayService = null!;
        
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            config.AssertConfigurationIsValid();            
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockWorkdayService = Substitute.For<IWorkdayService>();

            _controller = new WorkdayController(_mockWorkdayService);
        }

        [TestMethod]
        public async Task AddWorkdaysAsync_ReturnsEndupDate()
        {
            // Arrange      
            var endupDate = new DateTime(2004, 05, 14, 12, 00, 0);

            var workdayRequestDto = new WorkdayRequestDto
            {
                StartDate = new DateTime(2004, 05, 24, 18, 05, 0),
                Workdays = -5.5
            };
            
            _mockWorkdayService.AddWorkdaysAsync(Arg.Any<DateTime>(), Arg.Any<double>()).Returns(endupDate);

            // Act
            var result = await _controller.AddWorkdaysAsync(workdayRequestDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);            
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var resultValue = okResult.Value;
            Assert.IsInstanceOfType(resultValue, typeof(DateTime));
            Assert.AreEqual<DateTime>(endupDate, (DateTime)resultValue!);            
        }
    }
}
