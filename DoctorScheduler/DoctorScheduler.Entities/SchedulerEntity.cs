namespace DoctorScheduler.Entities
{
    public class SchedulerEntity
    {
        public FacilityEntity Facility { get; set; }

        public int SlotDurationMinutes { get; set; }

        public SlotEntity Monday { get; set; }

        public SlotEntity Tuesday { get; set; }

        public SlotEntity Wednesday { get; set; }

        public SlotEntity Thursday { get; set; }

        public SlotEntity Friday { get; set; }

        public SlotEntity Saturday { get; set; }

        public SlotEntity Sunday { get; set; }
    }
}
