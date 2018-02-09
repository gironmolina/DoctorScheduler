using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Exceptions;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastructure.Interfaces;
using DoctorScheduler.TestUtils.Builders.Entities;
using NUnit.Framework;
using Rhino.Mocks;
using Unity;

namespace DoctorScheduler.Domain.Tests.Services
{
    [TestFixture]
    public class SchedulerServiceTests
    {
        [SetUp]
        public void TestSetUp()
        {
            TestSetup.RegisterDependecies();
        }

        [Test]
        public void GetWeeklyAvailability_WithSlotDurationEqualsTo0_ShouldReturnExpectedException()
        {
            // Arrange
            TestSetup.MockDependency<ISchedulerRepository>();
            var schedulerService = TestSetup.Container.Resolve<ISchedulerService>();
            var schedulerRepository = TestSetup.Container.Resolve<ISchedulerRepository>();
            schedulerRepository.Stub(m => m.GetScheduler(Arg<string>.Is.Anything)).Return(Task.FromResult(new SchedulerEntity()));

            // Act / Assert
            Assert.CatchAsync<SchedulerServiceException>(async () => await schedulerService.GetWeeklyAvailability(Arg<string>.Is.Anything).ConfigureAwait(false), 
                                                                     "The slot duration cannot be lower or equals to 0.");
        }

        [Test]
        public async Task GetWeeklyAvailability_WithSlotDurationGreaterThan60AndDaysEmpty_ShouldReturnEmptyWeeks()
        {
            // Arrange
            TestSetup.MockDependency<ISchedulerRepository>();
            var schedulerService = TestSetup.Container.Resolve<ISchedulerService>();
            var schedulerRepository = TestSetup.Container.Resolve<ISchedulerRepository>();
            var schedulerEntity = new SchedulerEntityBuilder().SlotDurationMinutes(60).Build();
            schedulerRepository.Stub(m => m.GetScheduler(Arg<string>.Is.Anything)).Return(Task.FromResult(schedulerEntity));

            // Act
            var result = await schedulerService.GetWeeklyAvailability(Arg<string>.Is.Anything).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(0, result.WeekHours.Count);
        }

        [Test]
        public async Task GetWeeklyAvailability_WithValidDataAndWithBusySlots_ShouldReturnExpectedValues()
        {
            // Arrange
            const string facilityName = "Home Sweet Home";
            const string address = "Remote";
            TestSetup.MockDependency<ISchedulerRepository>();
            var schedulerService = TestSetup.Container.Resolve<ISchedulerService>();
            var schedulerRepository = TestSetup.Container.Resolve<ISchedulerRepository>();
            var schedulerEntity = new SchedulerEntityBuilder().WithDefaultValues().Build();
            schedulerRepository.Stub(m => m.GetScheduler(Arg<string>.Is.Anything)).Return(Task.FromResult(schedulerEntity));

            // Act
            var result = await schedulerService.GetWeeklyAvailability(Arg<string>.Is.Anything).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(facilityName, result.Facility.Name);
            Assert.AreEqual(address, result.Facility.Address);
            Assert.AreEqual(60, result.SlotDurationMinutes);
            Assert.AreEqual(new TimeSpan(9, 0, 0), result.WeekHours[0].Monday);
            Assert.AreEqual(new TimeSpan(11, 0, 0), result.WeekHours[1].Monday);
            Assert.AreEqual(new TimeSpan(12, 0, 0), result.WeekHours[2].Monday);
            Assert.AreEqual(new TimeSpan(14, 0, 0), result.WeekHours[3].Monday);
            Assert.AreEqual(new TimeSpan(15, 0, 0), result.WeekHours[4].Monday);
            Assert.AreEqual(new TimeSpan(16, 0, 0), result.WeekHours[5].Monday);
        }

        public async Task GetWeeklyAvailability_WithValidDataAndWithBusySlots_ShouldReturnExpectedValues2()
        {
            // Arrange
            const string facilityName = "Home Sweet Home";
            const string address = "Remote";
            TestSetup.MockDependency<ISchedulerRepository>();
            var schedulerService = TestSetup.Container.Resolve<ISchedulerService>();
            var schedulerRepository = TestSetup.Container.Resolve<ISchedulerRepository>();
            var schedulerEntity = new SchedulerEntityBuilder()
                .Monday(new SlotEntityBuilder().WithDefaultValues()
                .BusySlots(new List<BusySlotEntity>
                    {
                            new BusySlotEntityBuilder().WithDefaultValues().Build()
                    }).Build())
                .Build();
            schedulerRepository.Stub(m => m.GetScheduler(Arg<string>.Is.Anything)).Return(Task.FromResult(schedulerEntity));

            // Act
            var result = await schedulerService.GetWeeklyAvailability(Arg<string>.Is.Anything).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(facilityName, result.Facility.Name);
            Assert.AreEqual(address, result.Facility.Address);
            Assert.AreEqual(60, result.SlotDurationMinutes);
            Assert.AreEqual(new TimeSpan(9, 0, 0), result.WeekHours[0].Monday);
            Assert.AreEqual(new TimeSpan(11, 0, 0), result.WeekHours[1].Monday);
            Assert.AreEqual(new TimeSpan(12, 0, 0), result.WeekHours[2].Monday);
            Assert.AreEqual(new TimeSpan(14, 0, 0), result.WeekHours[3].Monday);
            Assert.AreEqual(new TimeSpan(15, 0, 0), result.WeekHours[4].Monday);
            Assert.AreEqual(new TimeSpan(16, 0, 0), result.WeekHours[5].Monday);
        }
    }
}
