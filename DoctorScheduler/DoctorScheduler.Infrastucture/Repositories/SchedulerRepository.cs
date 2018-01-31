using System;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastucture.Interfaces;

namespace DoctorScheduler.Infrastucture.Repositories
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
            var url = $"{appConfigSettings.SchedulerApiUrl}/GetWeeklyAvailability/{date}";
            //Logger.DebugFormat("Getting weekly availability for: {0}", date);
            return await HttpClientHelpers.GetAsync<SchedulerEntity>(url, 
                appConfigSettings.Username, 
                appConfigSettings.Password).ConfigureAwait(false);
        }

        public async Task<bool> PostSlot(TakeSlotEntity slot)
        {
            var url = $"{appConfigSettings.SchedulerApiUrl}/TakeSlot";
            //Logger.DebugFormat("Post slot: {0}", JsonConvert.SerializeObject(slot));
            return await HttpClientHelpers.PostAsync(url, 
                appConfigSettings.Username, 
                appConfigSettings.Password, 
                slot).ConfigureAwait(false);
        }
    }
}
