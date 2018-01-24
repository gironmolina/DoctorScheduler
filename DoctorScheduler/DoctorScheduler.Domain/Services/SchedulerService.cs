using System.Configuration;
using System.Threading.Tasks;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastucture.Helpers;
using log4net;
using Newtonsoft.Json;

namespace DoctorScheduler.Domain.Services
{
    public class SchedulerService : ISchedulerService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchedulerService));
        private static readonly string SchedulerUrl = ConfigurationManager.AppSettings["SchedulerAPIUrl"];

        public async Task<SchedulerEntity> GetWeeklyAvailability(string date)
        {
            var url = $"{SchedulerUrl}/GetWeeklyAvailability/{date}";
            Logger.DebugFormat("Getting weekly availability for: {0}", date);
            return await HttpClientHelpers.GetAsync<SchedulerEntity>(url).ConfigureAwait(false);
        }

        public async Task<bool> TakeSlot(TakeSlotEntity slot)
        {
            var url = $"{SchedulerUrl}/TakeSlot";
            Logger.DebugFormat("Taking slot: {0}", JsonConvert.SerializeObject(slot));
            return await HttpClientHelpers.PostAsync(url, slot).ConfigureAwait(false);
        }
    }
}
