using System.Collections.Generic;

namespace DoctorScheduler.Entities
{
    public class SlotEntity
    {
        public WorkPeriodEntity WorkPeriod { get; set; }

        public List<BusySlotEntity> BusySlots { get; set; }
    }
}
