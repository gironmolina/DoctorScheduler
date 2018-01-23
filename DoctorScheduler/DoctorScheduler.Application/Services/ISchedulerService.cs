using System.Threading.Tasks;

namespace DoctorScheduler.Application.Services
{
    public interface ISchedulerService
    {
        Task<dynamic> GetWeeklyAvailability();
    }
}
