using System.Threading.Tasks;
using DoctorScheduler.Entities;

namespace DoctorScheduler.Domain.Services
{
    public interface ISchedulerService
    {
        Task<SchedulerEntity> GetWeeklyAvailability(string date);

        Task<bool> TakeSlot(TakeSlotEntity slot);
    }
}
