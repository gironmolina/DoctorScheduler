using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class SchedulerDtoBuilder
    {
        private readonly SchedulerDto innerObject = new SchedulerDto();
        
        public SchedulerDtoBuilder WithDefaultValues()
        {
            return this
                .Facility(new FacilityDtoBuilder().WithDefaultValues().Build())
                .SlotDurationMinutes(10)
                .Monday(new SlotDtoBuilder().WithDefaultValues().Build())
                .Wednesday(new SlotDtoBuilder().WithDefaultValues().Build())
                .Friday(new SlotDtoBuilder().WithDefaultValues().Build());
        }

        public SchedulerDtoBuilder Facility(FacilityDto facility)
        {
            this.innerObject.Facility = facility;
            return this;
        }

        public SchedulerDtoBuilder SlotDurationMinutes(int slotDurationMinutes)
        {
            this.innerObject.SlotDurationMinutes = slotDurationMinutes;
            return this;
        }

        public SchedulerDtoBuilder Monday(SlotDto monday)
        {
            this.innerObject.Monday = monday;
            return this;
        }

        public SchedulerDtoBuilder Tuesday(SlotDto tuesday)
        {
            this.innerObject.Tuesday = tuesday;
            return this;
        }
        
        public SchedulerDtoBuilder Wednesday(SlotDto wednesday)
        {
            this.innerObject.Wednesday = wednesday;
            return this;
        }

        public SchedulerDtoBuilder Thursday(SlotDto thursday)
        {
            this.innerObject.Thursday = thursday;
            return this;
        }

        public SchedulerDtoBuilder Friday(SlotDto friday)
        {
            this.innerObject.Friday = friday;
            return this;
        }

        public SchedulerDtoBuilder Saturday(SlotDto saturday)
        {
            this.innerObject.Saturday = saturday;
            return this;
        }

        public SchedulerDtoBuilder Sunday(SlotDto sunday)
        {
            this.innerObject.Sunday = sunday;
            return this;
        }

        public SchedulerDto Build()
        {
            return this.innerObject;
        }
    }
}
