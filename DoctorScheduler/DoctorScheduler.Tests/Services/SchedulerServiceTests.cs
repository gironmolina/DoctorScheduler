using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.API.Controllers;
using DoctorScheduler.IntegrationTests.Extensions;
using DoctorScheduler.Tests.Builders;
using NUnit.Framework;

namespace DoctorScheduler.IntegrationTests.Services
{
    [TestFixture]
    public class SchedulerServiceTests
    {
        [Test]
        public async Task GetWeeklyAvailability_WithValidDate_ShouldReturnExpectedResult()
        {
            // Arrange
            const string date = "20180101";
            var controller = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await controller.GetWeeklyAvailability(date).ConfigureAwait(false);
            var actualDto = (response as OkNegotiatedContentResult<SchedulerWeekDto>)?.Content;
            var isSuccessStatusCode = response.ExecuteAsync(new CancellationToken()).Result.IsSuccessStatusCode;

            // Assert
            Assert.IsTrue(isSuccessStatusCode);
            Assert.IsInstanceOf(typeof(SchedulerWeekDto), actualDto);
        }

        [Test]
        [TestCase("WeekHours")]
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

        private TakeSlotDto GetTestTakeSlotDto()
        {
            return new TakeSlotDtoBuilder().WithDefaultValues().Build();
        }
    }
}