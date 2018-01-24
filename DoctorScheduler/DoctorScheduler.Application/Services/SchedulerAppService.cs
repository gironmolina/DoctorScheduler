﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Domain.Services;
using DoctorScheduler.Entities;

namespace DoctorScheduler.Application.Services
{
    public class SchedulerAppService : ISchedulerAppService
    {
        private readonly ISchedulerService schedulerService;

        public SchedulerAppService(ISchedulerService schedulerService)
        {
            this.schedulerService = schedulerService ?? throw new ArgumentNullException(nameof(schedulerService));
        }

        public async Task<SchedulerDto> GetWeeklyAvailabilityAdapter(string date)
        {
            var schedulerEntity = await this.schedulerService.GetWeeklyAvailability(date);
            return Mapper.Map<SchedulerDto>(schedulerEntity);
        }

        public async Task<bool> TakeSlotAdapter(TakeSlotDto slot)
        {
            var slotEntity = Mapper.Map<TakeSlotEntity>(slot);
            return await this.schedulerService.TakeSlot(slotEntity);
        }
    }
}