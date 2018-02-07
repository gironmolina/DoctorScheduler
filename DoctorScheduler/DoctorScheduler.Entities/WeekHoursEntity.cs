using System;

namespace DoctorScheduler.Entities
{
    public class WeekHoursEntity
    {
        public DateTime? Monday { get; set; }

        public DateTime? Tuesday { get; set; }

        public DateTime? Wednesday { get; set; }

        public DateTime? Thursday { get; set; }

        public DateTime? Friday { get; set; }

        public DateTime? Saturday { get; set; }

        public DateTime? Sunday { get; set; }
    }
}