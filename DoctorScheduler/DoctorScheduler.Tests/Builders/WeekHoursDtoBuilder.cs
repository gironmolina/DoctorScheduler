using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
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

        public WeekHoursDtoBuilder Monday(int? monday)
        {
            this.innerObject.Monday = monday;
            return this;
        }

        public WeekHoursDtoBuilder Tuesday(int? tuesday)
        {
            this.innerObject.Tuesday = tuesday;
            return this;
        }

        public WeekHoursDtoBuilder Wednesday(int? wednesday)
        {
            this.innerObject.Wednesday = wednesday;
            return this;
        }

        public WeekHoursDtoBuilder Thursday(int? thursday)
        {
            this.innerObject.Thursday = thursday;
            return this;
        }

        public WeekHoursDtoBuilder Friday(int? friday)
        {
            this.innerObject.Friday = friday;
            return this;
        }

        public WeekHoursDtoBuilder Saturday(int? saturday)
        {
            this.innerObject.Saturday = saturday;
            return this;
        }

        public WeekHoursDtoBuilder Sunday(int? sunday)
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