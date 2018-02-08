using System;
using DoctorScheduler.Entities;

namespace DoctorScheduler.TestUtils.Builders.Entities
{
    public class BusySlotEntityBuilder
    {
        private readonly BusySlotEntity innerObject = new BusySlotEntity();

        public BusySlotEntityBuilder WithDefaultValues()
        {
            return null;
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
