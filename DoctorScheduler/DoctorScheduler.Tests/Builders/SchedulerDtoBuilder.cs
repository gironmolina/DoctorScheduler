using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class SchedulerDtoBuilder
    {
        private readonly SchedulerDto innerObject = new SchedulerDto();
        
        public SchedulerDtoBuilder WithDefaultValues()
        {
            return this
                .Facility(new FacilityDto())
                .SlotDurationMinutes(1)
                .Monday(new SlotDto())
                .Tuesday(new SlotDto())
                .Wednesday(new SlotDto())
                .Thursday(new SlotDto())
                .Friday(new SlotDto())
                .Saturday(new SlotDto())
                .Sunday(new SlotDto());
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
