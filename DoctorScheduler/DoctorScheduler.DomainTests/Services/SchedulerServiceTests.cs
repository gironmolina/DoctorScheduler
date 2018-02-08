using System.Threading.Tasks;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastructure.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using Unity;

namespace DoctorScheduler.Domain.Tests.Services
{
    [TestFixture]
    public class SchedulerServiceTests
    {
        [Test]
        public void ConvertCurrency_InvalidCurrency_ShouldReturnRateConvertException()
        {
            // Arrange
            var schedulerService = TestSetup.Container.Resolve<ISchedulerService>();
            var schedulerRepository = TestSetup.Container.Resolve<ISchedulerRepository>();
            schedulerRepository.Stub(m => m.GetScheduler(Arg<string>.Is.Anything)).Return(Task.FromResult(new SchedulerEntity()));

            var asd = schedulerService.GetWeeklyAvailability("");

            // Act / Assert
            //Assert.Catch<RateConvertException>(() =>
            //    schedulerService.Convert(Currencies.Euro, transactions, GetRates()));
        }
    }
}
