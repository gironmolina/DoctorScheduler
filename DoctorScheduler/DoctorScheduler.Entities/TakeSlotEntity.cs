using System;

namespace DoctorScheduler.Entities
{
    public class TakeSlotEntity
    {
        public string FacilityId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public PatientEntity Patient { get; set; }

        public string Comments { get; set; }
    }
}
