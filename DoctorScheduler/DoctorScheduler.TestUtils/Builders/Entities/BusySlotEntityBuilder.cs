using System;
using DoctorScheduler.Entities;

namespace DoctorScheduler.TestUtils.Builders.Entities
{
    public class BusySlotEntityBuilder
    {
        private readonly BusySlotEntity innerObject = new BusySlotEntity();

        public BusySlotEntityBuilder WithDefaultValues()
        {
            return this.Start(new DateTime().Date.AddHours(10))
                .End(new DateTime().Date.AddHours(11));
        }

        public BusySlotEntityBuilder Start(DateTime start)
        {
            this.innerObject.Start = start;
            return this;
        }

        public BusySlotEntityBuilder End(DateTime end)
        {
            this.innerObject.End = end;
            return this;
        }

        public BusySlotEntity Build()
        {
            return this.innerObject;
        }
    }
}
