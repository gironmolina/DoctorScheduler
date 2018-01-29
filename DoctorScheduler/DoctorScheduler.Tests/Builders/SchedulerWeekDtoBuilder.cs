using System.Collections.Generic;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class SchedulerWeekDtoBuilder
    {
        private readonly SchedulerWeekDto innerObject = new SchedulerWeekDto();

        public SchedulerWeekDtoBuilder WithDefaultValues()
        {
            return this
                .Facility(new FacilityDtoBuilder().WithDefaultValues().Build())
                .SlotDurationMinutes(10)
                .WeekHours(new List<WeekHoursDto>
                {
                    new WeekHoursDtoBuilder().WithDefaultValues().Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(9).Wednesday(9).Friday(9).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(10).Wednesday(10).Friday(10).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(11).Wednesday(11).Friday(11).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(12).Wednesday(12).Friday(12).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(14).Wednesday(14).Friday(14).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(15).Wednesday(15).Friday(15).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(16).Wednesday(16).Friday(16).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(17).Wednesday(17).Friday(null).Build()
                });
        }

        public SchedulerWeekDtoBuilder Facility(FacilityDto facility)
        {
            this.innerObject.Facility = facility;
            return this;
        }

        public SchedulerWeekDtoBuilder SlotDurationMinutes(int slotDurationMinutes)
        {
            this.innerObject.SlotDurationMinutes = slotDurationMinutes;
            return this;
        }

        public SchedulerWeekDtoBuilder WeekHours(List<WeekHoursDto> weekHours)
        {
            this.innerObject.WeekHours = weekHours;
            return this;
        }

        public SchedulerWeekDto Build()
        {
            return this.innerObject;
        }
    }
}
