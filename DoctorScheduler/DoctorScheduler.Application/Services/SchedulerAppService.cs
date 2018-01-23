using System;
using System.Threading.Tasks;
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
            var response = await this.schedulerService.GetWeeklyAvailability(date);
            return response;
        }

        public async Task<dynamic> TakeSlotAdapter()
        {
            var response = await this.schedulerService.TakeSlot();
            return response;
        }
    }
}
