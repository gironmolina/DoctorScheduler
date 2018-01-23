using System;

namespace DoctorScheduler.Application.Dtos
{
    public class TakeSlotDto
    {
        public string FacilityId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public PatientDto Patient { get; set; }

        public string Comments { get; set; }
    }
}
