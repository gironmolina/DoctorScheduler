using System.Collections.Generic;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Tests.Builders;

namespace DoctorScheduler.IntegrationTests.Builders
{
    public class SlotDtoBuilder
    {
        private readonly SlotDto innerObject = new SlotDto();

        public SlotDtoBuilder WithDefaultValues()
        {
            return this
                .WorkPeriod(new WorkPeriodDtoBuilder().WithDefaultValues().Build())
                .BusySlots(new List<BusySlotDto>());
        }

        public SlotDtoBuilder BusySlots(List<BusySlotDto> busySlots)
        {
            this.innerObject.BusySlots = busySlots;
            return this;
        }

        public SlotDtoBuilder WorkPeriod(WorkPeriodDto workPeriod)
        {
            this.innerObject.WorkPeriod = workPeriod;
            return this;
        }

        public SlotDto Build()
        {
            return this.innerObject;
        }
    }
}
