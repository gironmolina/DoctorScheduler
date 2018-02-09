using System.Collections.Generic;
using DoctorScheduler.Entities;

namespace DoctorScheduler.TestUtils.Builders.Entities
{
    public class SlotEntityBuilder
    {
        private readonly SlotEntity innerObject = new SlotEntity();

        public SlotEntityBuilder WithDefaultValues()
        {
            return WorkPeriod(new WorkPeriodEntityBuilder().WithDefaultValues()
                    .Build())
                    .BusySlots(new List<BusySlotEntity>
                    {
                        new BusySlotEntityBuilder().WithDefaultValues()
                            .Build()
                    });
        }

        public SlotEntityBuilder BusySlots(List<BusySlotEntity> busySlots)
        {
            this.innerObject.BusySlots = busySlots;
            return this;
        }

        public SlotEntityBuilder WorkPeriod(WorkPeriodEntity workPeriod)
        {
            this.innerObject.WorkPeriod = workPeriod;
            return this;
        }

        public SlotEntity Build()
        {
            return this.innerObject;
        }
    }
}
