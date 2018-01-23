using System.Net.Http;
using System.Threading.Tasks;
using DoctorScheduler.Entities;

namespace DoctorScheduler.Domain.Services
{
    public interface ISchedulerService
    {
        Task<SchedulerEntity> GetWeeklyAvailability(string date);

        Task<HttpResponseMessage> TakeSlot(TakeSlotEntity slot);
    }
}
