using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.API.Controllers;
using DoctorScheduler.Tests.Builders;
using DoctorScheduler.Tests.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DoctorScheduler.Tests.Services
{
    [TestFixture]
    public class SchedulerServiceTests
    {
        [Test]
        public async Task GetWeeklyAvailability_WithValidDate_ShouldReturnExpectedResult()
        {
            // Arrange
            const string date = "20180101";
            var schedulerDto = this.GetTestSchedulerDto();
            var controller = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await controller.GetWeeklyAvailability(date).ConfigureAwait(false);
            var actualDto = (response as OkNegotiatedContentResult<SchedulerDto>)?.Content;
            var expectedJson = JsonConvert.SerializeObject(schedulerDto);
            var actualJson = JsonConvert.SerializeObject(actualDto);

            // Assert
            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        [TestCase("DayHours")]
        [TestCase("2018")]
        public async Task GetWeeklyAvailability_WithInvalidDate_ShouldReturnBadRequest(string date)
        {
            // Arrange
            var controller = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await controller.GetWeeklyAvailability(date).ConfigureAwait(false);
            var resultStatusCode = response.ExecuteAsync(new CancellationToken()).Result.StatusCode;

            // Assert
            Assert.AreEqual(resultStatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task TakeSlot_WithValidDto_ShouldReturnSuccessStatus()
        {
            // Arrange
            var takeSlotDto = this.GetTestTakeSlotDto();
            var controller = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await controller.TakeSlot(takeSlotDto).Result.ExecuteAsync(new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async Task TakeSlot_WithInvalidDto_ShouldReturnBadRequest()
        {
            // Arrange
            var controller = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await controller.TakeSlot(new TakeSlotDto()).Result.ExecuteAsync(new CancellationToken()).ConfigureAwait(false);

            // Assert
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        private SchedulerDto GetTestSchedulerDto()
        {
            var builder = new SchedulerDtoBuilder()
                .WithDefaultValues()
                .Friday(new SlotDtoBuilder()
                    .WithDefaultValues()
                    .WorkPeriod(new WorkPeriodDtoBuilder()
                        .WithDefaultValues()
                        .StartHour(8)
                        .EndHour(16).Build())
                    .BusySlots(new List<BusySlotDto>
                    {
                        new BusySlotDtoBuilder().WithDefaultValues().Build()
                    }).Build());

            return builder.Build();
        }

        private TakeSlotDto GetTestTakeSlotDto()
        {
            return new TakeSlotDtoBuilder().WithDefaultValues().Build();
        }
    }
}