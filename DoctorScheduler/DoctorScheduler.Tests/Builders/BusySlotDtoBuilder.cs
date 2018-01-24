using System;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class BusySlotDtoBuilder
    {
        private readonly BusySlotDto innerObject = new BusySlotDto();

        public BusySlotDtoBuilder WithDefaultValues()
        {
            return this
                .Start(new DateTime(2018, 1, 5, 8, 0, 0))
                .End(new DateTime(2018, 1, 5, 8, 10, 0));
        }

        public BusySlotDtoBuilder Start(DateTime start)
        {
            this.innerObject.Start = start;
            return this;
        }

        public BusySlotDtoBuilder End(DateTime end)
        {
            this.innerObject.End = end;
            return this;
        }

        public BusySlotDto Build()
        {
            return this.innerObject;
        }
    }
}
