using System.Threading.Tasks;

namespace DoctorScheduler.Application.Services
{
    public interface ISchedulerAppService
    {
        Task<dynamic> GetWeeklyAvailabilityAdapter(string date);

        Task<dynamic> TakeSlotAdapter();
    }
}
