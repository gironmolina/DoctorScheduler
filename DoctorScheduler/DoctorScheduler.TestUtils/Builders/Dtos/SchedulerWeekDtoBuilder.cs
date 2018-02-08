using System;
using System.Collections.Generic;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.TestUtils.Builders.Dtos
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
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(9, 0, 0)).Wednesday(new TimeSpan(9, 0, 0)).Friday(new TimeSpan(9, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(10, 0, 0)).Wednesday(new TimeSpan(10, 0, 0)).Friday(new TimeSpan(10, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(11, 0, 0)).Wednesday(new TimeSpan(11, 0, 0)).Friday(new TimeSpan(11, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(12, 0, 0)).Wednesday(new TimeSpan(12, 0, 0)).Friday(new TimeSpan(12, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(14, 0, 0)).Wednesday(new TimeSpan(14, 0, 0)).Friday(new TimeSpan(14, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(15, 0, 0)).Wednesday(new TimeSpan(15, 0, 0)).Friday(new TimeSpan(15, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(16, 0, 0)).Wednesday(new TimeSpan(16, 0, 0)).Friday(new TimeSpan(16, 0, 0)).Build(),
                    new WeekHoursDtoBuilder().WithDefaultValues().Monday(new TimeSpan(17, 0, 0)).Wednesday(new TimeSpan(17, 0, 0)).Friday(null).Build()
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
