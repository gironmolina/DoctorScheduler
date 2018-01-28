using System.Collections.Generic;

namespace DoctorScheduler.Entities
{
    public class SchedulerWeekEntity
    {
        public FacilityEntity Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public List<WeekHoursEntity> WeekHours { get; set; }
    }
}