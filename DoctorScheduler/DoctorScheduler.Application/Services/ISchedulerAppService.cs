﻿using System.Threading.Tasks;
using DoctorScheduler.Application.Dtos;

namespace DoctorScheduler.Application.Services
{
    public interface ISchedulerAppService
    {
        Task<SchedulerWeekDto> GetWeeklyAvailabilityAdapter(string date);

        Task<bool> TakeSlotAdapter(TakeSlotDto slot);
    }
}
