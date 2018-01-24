using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.API.Controllers;
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
        [TestCase("Test")]
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
            return new SchedulerDto
            {
                Facility = new FacilityDto
                {
                    FacilityId = "e9f7bd81-965d-4464-b607-999112b56022",
                    Name = "Las Palmeras",
                    Address = "Plaza de la independencia 36, 38006 Santa Cruz de Tenerife"
                },
                SlotDurationMinutes = 10,
                Monday = new SlotDto
                {
                    WorkPeriod = new WorkPeriodDto
                    {
                        StartHour = 9,
                        EndHour = 17,
                        LunchStartHour = 13,
                        LunchEndHour = 14
                    },
                    BusySlots = new List<BusySlotDto>()
                    
                },
                Wednesday = new SlotDto
                {
                    WorkPeriod = new WorkPeriodDto
                    {
                        StartHour = 9,
                        EndHour = 17,
                        LunchStartHour = 13,
                        LunchEndHour = 14
                    },
                    BusySlots = new List<BusySlotDto>()
                },
                Friday = new SlotDto
                {
                    WorkPeriod = new WorkPeriodDto
                    {
                        StartHour = 8,
                        EndHour = 16,
                        LunchStartHour = 13,
                        LunchEndHour = 14
                    },
                    BusySlots = new List<BusySlotDto>
                    {
                        new BusySlotDto
                        {
                            Start = new DateTime(2018,1,5,8,0,0),
                            End = new DateTime(2018,1,5,8,10,0)
                        }
                    }
                }
            };
        }

        private TakeSlotDto GetTestTakeSlotDto()
        {
            return new TakeSlotDto
            {
                FacilityId = "e9f7bd81-965d-4464-b607-999112b56022",
                Start = new DateTime(2017, 06, 13, 11, 0, 0),
                End = new DateTime(2017, 06, 13, 12, 0, 0),
                Patient = new PatientDto
                {
                    Name = "Mario",
                    SecondName = "Neta",
                    Email = "mario@myspace.es",
                    Phone = "555 44 33 22"
                },
                Comments = "my arm hurts a lot"
            };
        }
    }
}