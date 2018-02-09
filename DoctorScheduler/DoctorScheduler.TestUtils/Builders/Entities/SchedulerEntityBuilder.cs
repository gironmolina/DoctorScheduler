using DoctorScheduler.Entities;

namespace DoctorScheduler.TestUtils.Builders.Entities
{

    public class SchedulerEntityBuilder
    {
        private readonly SchedulerEntity innerObject = new SchedulerEntity();

        public SchedulerEntityBuilder WithDefaultValues()
        {
            return this.Facility(new FacilityEntity
                {
                    Name = "Home Sweet Home",
                    Address = "Remote"
                })
                .SlotDurationMinutes(60)
                .Monday(new SlotEntityBuilder().WithDefaultValues()
                    .Build());
            
        }

        public SchedulerEntityBuilder Facility(FacilityEntity facility)
        {
            this.innerObject.Facility = facility;
            return this;
        }

        public SchedulerEntityBuilder SlotDurationMinutes(int slotDurationMinutes)
        {
            this.innerObject.SlotDurationMinutes = slotDurationMinutes;
            return this;
        }

        public SchedulerEntityBuilder Monday(SlotEntity monday)
        {
            this.innerObject.Monday = monday;
            return this;
        }

        public SchedulerEntityBuilder Tuesday(SlotEntity tuesday)
        {
            this.innerObject.Tuesday = tuesday;
            return this;
        }

        public SchedulerEntityBuilder Wednesday(SlotEntity wednesday)
        {
            this.innerObject.Wednesday = wednesday;
            return this;
        }

        public SchedulerEntityBuilder Thursday(SlotEntity thursday)
        {
            this.innerObject.Thursday = thursday;
            return this;
        }

        public SchedulerEntityBuilder Friday(SlotEntity friday)
        {
            this.innerObject.Friday = friday;
            return this;
        }

        public SchedulerEntityBuilder Saturday(SlotEntity saturday)
        {
            this.innerObject.Saturday = saturday;
            return this;
        }

        public SchedulerEntityBuilder Sunday(SlotEntity sunday)
        {
            this.innerObject.Sunday = sunday;
            return this;
        }

        public SchedulerEntity Build()
        {
            return this.innerObject;
        }
    }
}
