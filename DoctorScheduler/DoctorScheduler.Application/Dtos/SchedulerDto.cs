namespace DoctorScheduler.Application.Dtos
{
    public class SchedulerDto
    {
        public FacilityDto Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public SlotDto Monday { get; set; }

        public SlotDto Tuesday { get; set; }

        public SlotDto Wednesday { get; set; }

        public SlotDto Thursday { get; set; }

        public SlotDto Friday { get; set; }

        public SlotDto Saturday { get; set; }

        public SlotDto Sunday { get; set; }
    }
}
