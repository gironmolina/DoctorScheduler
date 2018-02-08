using System;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.IntegrationTests.Builders
{
    public class WeekHoursDtoBuilder
    {
        private readonly WeekHoursDto innerObject = new WeekHoursDto();

        public WeekHoursDtoBuilder WithDefaultValues()
        {
            return this
                .Monday(null)
                .Tuesday(null)
                .Wednesday(null)
                .Thursday(null)
                .Friday(null)
                .Saturday(null)
                .Sunday(null);
        }

        public WeekHoursDtoBuilder Monday(TimeSpan? monday)
        {
            this.innerObject.Monday = monday;
            return this;
        }

        public WeekHoursDtoBuilder Tuesday(TimeSpan? tuesday)
        {
            this.innerObject.Tuesday = tuesday;
            return this;
        }

        public WeekHoursDtoBuilder Wednesday(TimeSpan? wednesday)
        {
            this.innerObject.Wednesday = wednesday;
            return this;
        }

        public WeekHoursDtoBuilder Thursday(TimeSpan? thursday)
        {
            this.innerObject.Thursday = thursday;
            return this;
        }

        public WeekHoursDtoBuilder Friday(TimeSpan? friday)
        {
            this.innerObject.Friday = friday;
            return this;
        }

        public WeekHoursDtoBuilder Saturday(TimeSpan? saturday)
        {
            this.innerObject.Saturday = saturday;
            return this;
        }

        public WeekHoursDtoBuilder Sunday(TimeSpan? sunday)
        {
            this.innerObject.Sunday = sunday;
            return this;
        }

        public WeekHoursDto Build()
        {
            return this.innerObject;
        }
    }
}