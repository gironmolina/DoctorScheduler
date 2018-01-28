using System.Collections.Generic;

namespace DoctorScheduler.Application.Dtos
{
    public class SchedulerWeekDto
    {
        public FacilityDto Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public List<WeekHoursDto> WeekHours { get; set; }
    }
}
