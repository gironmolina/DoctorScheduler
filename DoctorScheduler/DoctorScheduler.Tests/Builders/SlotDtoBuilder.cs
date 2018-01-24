using System.Collections.Generic;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Tests.Builders
{
    public class SlotDtoBuilder
    {
        private readonly SlotDto innerObject = new SlotDto();

        public SlotDtoBuilder WithDefaultValues()
        {
            return this
                .BusySlots(new List<BusySlotDto>())
                .WorkPeriod(new WorkPeriodDto());
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
