using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendar.API.Tests
{
    [TestClass]
    public class AutoMapperProfileTests
    {
        private IMapper _mapper { get; set; } = null!;

        [TestInitialize]
        public void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void AutoMapperProfile_Configuration_IsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
