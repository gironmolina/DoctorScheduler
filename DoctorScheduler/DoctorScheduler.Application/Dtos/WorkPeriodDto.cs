﻿namespace DoctorScheduler.Application.Dtos
{
    public class WorkPeriodDto
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int LunchStartHour { get; set; }

        public int LunchEndHour { get; set; }
    }
}