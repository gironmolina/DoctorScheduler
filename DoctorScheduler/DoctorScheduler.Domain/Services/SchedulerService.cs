using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using DoctorScheduler.Entities;
using DoctorScheduler.Infrastucture.Helpers;

namespace DoctorScheduler.Domain.Services
{
    public class SchedulerService : ISchedulerService
    {
        private static readonly string SchedulerUrl = ConfigurationManager.AppSettings["SchedulerAPIUrl"];

        public async Task<SchedulerEntity> GetWeeklyAvailability(string date)
        {
            var url = $"{SchedulerUrl}/GetWeeklyAvailability/{date}";
            return await HttpClientHelpers.GetAsync<SchedulerEntity>(url).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> TakeSlot(TakeSlotEntity slot)
        {
            var url = $"{SchedulerUrl}/TakeSlot";
            return await HttpClientHelpers.PostAsync(url, slot).ConfigureAwait(false);
        }
    }
}
