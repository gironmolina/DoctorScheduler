﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Application.Interfaces;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Entities;
using log4net;

namespace DoctorScheduler.Application.Services
{
    public class SchedulerAppService : ISchedulerAppService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchedulerAppService));
        private readonly ISchedulerService schedulerService;

        public SchedulerAppService(ISchedulerService schedulerService)
        {
            this.schedulerService = schedulerService ?? throw new ArgumentNullException(nameof(schedulerService));
        }

        public async Task<SchedulerWeekDto> GetWeeklyAvailabilityAdapter(string date)
        {
            var schedulerEntity = await this.schedulerService.GetWeeklyAvailability(date);
            Logger.Debug("Mapping weekly availability response");
            return Mapper.Map<SchedulerWeekDto>(schedulerEntity);
        }

        public async Task<bool> TakeSlotAdapter(TakeSlotDto slot)
        {
            Logger.Debug("Mapping take slot request");
            var slotEntity = Mapper.Map<TakeSlotEntity>(slot);
            return await this.schedulerService.TakeSlot(slotEntity);
        }
    }
}
