using System.Threading.Tasks;
using DoctorScheduler.Entities;

namespace DoctorScheduler.Domain.Interfaces
{
    public interface ISchedulerService
    {
        Task<SchedulerWeekEntity> GetWeeklyAvailability(string date);

        Task<bool> TakeSlot(TakeSlotEntity slot);
    }
}
