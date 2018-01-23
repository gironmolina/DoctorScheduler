using System;
using System.Threading.Tasks;
using AutoMapper;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Domain.Services;

namespace DoctorScheduler.Application.Services
{
    public class SchedulerAppService : ISchedulerAppService
    {
        private readonly ISchedulerService schedulerService;

        public SchedulerAppService(ISchedulerService schedulerService)
        {
            this.schedulerService = schedulerService ?? throw new ArgumentNullException(nameof(schedulerService));
        }

        public async Task<dynamic> GetWeeklyAvailabilityAdapter(string date)
        {
            var schedulerEntity = await this.schedulerService.GetWeeklyAvailability(date);
            var schedulerDto = Mapper.Map<SchedulerDto>(schedulerEntity);
            return schedulerDto;
        }

        public async Task<dynamic> TakeSlotAdapter()
        {
            var response = await this.schedulerService.TakeSlot();
            return response;
        }
    }
}
