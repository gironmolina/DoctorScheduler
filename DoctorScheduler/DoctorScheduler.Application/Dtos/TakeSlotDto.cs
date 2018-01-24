using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorScheduler.Application.Dtos
{
    public class TakeSlotDto
    {
        [Required]
        public string FacilityId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public PatientDto Patient { get; set; }
        
        public string Comments { get; set; }
    }
}
