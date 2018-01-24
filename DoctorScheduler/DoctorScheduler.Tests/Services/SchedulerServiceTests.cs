using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
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
        public async Task Test1()
        {
            // Arrange
            const string date = "20180101";
            var schedulerDto = this.GetTestSchedulerDto();
            var requestController = TestSetup.Container.GetController<SchedulerController>();

            // Act
            var response = await requestController.GetWeeklyAvailability(date).ConfigureAwait(false);
            var actualDto = (response as OkNegotiatedContentResult<SchedulerDto>)?.Content;
            var expectedJson = JsonConvert.SerializeObject(schedulerDto);
            var actualJson = JsonConvert.SerializeObject(actualDto);

            //Assert
            Assert.AreEqual(expectedJson, actualJson);
        }

        private SchedulerDto GetTestSchedulerDto()
        {
            var schedulerDto = new SchedulerDto
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

            return schedulerDto;
        }
    }
}
