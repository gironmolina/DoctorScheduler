using System.Collections.Generic;

namespace DoctorScheduler.Application.Dtos
{
    public class SlotDto
    {
        public WorkPeriodDto WorkPeriod { get; set; }

        public List<BusySlotDto> BusySlots { get; set; }
    }
}