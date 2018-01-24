using System.ComponentModel.DataAnnotations;

namespace DoctorScheduler.Application.Dtos
{
    public class PatientDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}