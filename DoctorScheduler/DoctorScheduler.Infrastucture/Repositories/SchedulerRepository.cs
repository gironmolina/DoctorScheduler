using System;
using System.Threading.Tasks;
using DoctorScheduler.CrossCutting.Exceptions;
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
            var url = $"{appConfigSettings.SchedulerApiUrl}/GetWeeklyAvailability/{date}";
            try
            {
                return await HttpClientHelpers.GetAsync<SchedulerEntity>(
                    url,
                    appConfigSettings.Username,
                    appConfigSettings.Password).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new SchedulerBadRequestException(e.Message, e);
            }
            
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
