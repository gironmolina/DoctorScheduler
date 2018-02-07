using System;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastructure.Interfaces;

namespace DoctorScheduler.Infrastructure.Repositories
{
    public class SchedulerRepository : ISchedulerRepository
    {
        private readonly IAppConfigSettings appConfigSettings;

        public SchedulerRepository(IAppConfigSettings appConfigSettings)
        {
            this.appConfigSettings = appConfigSettings ?? throw new ArgumentNullException(nameof(appConfigSettings));
        }

        public async Task<SchedulerEntity> GetScheduler(string date)
        {
            var url = $"{this.appConfigSettings.SchedulerApiUrl}/GetWeeklyAvailability/{date}";
            return await HttpClientHelpers.GetAsync<SchedulerEntity>(
                url, 
                this.appConfigSettings.Username, 
                this.appConfigSettings.Password).ConfigureAwait(false);
        }

        public async Task<bool> PostSlot(TakeSlotEntity slot)
        {
            var url = $"{this.appConfigSettings.SchedulerApiUrl}/TakeSlot";
            //Logger.DebugFormat("Post slot: {0}", JsonConvert.SerializeObject(slot));
            return await HttpClientHelpers.PostAsync(
                url, 
                this.appConfigSettings.Username, 
                this.appConfigSettings.Password, 
                slot).ConfigureAwait(false);
        }
    }
}
