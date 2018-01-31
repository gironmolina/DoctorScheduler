using System.Threading.Tasks;
using DoctorScheduler.Entities;

namespace DoctorScheduler.Infrastucture.Interfaces
{
    public interface ISchedulerRepository
    {
        Task<SchedulerEntity> GetScheduler(string date);

        Task<bool> PostSlot(TakeSlotEntity slot);
    }
}
