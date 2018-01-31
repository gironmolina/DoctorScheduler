namespace DoctorScheduler.CrossCutting.Interfaces
{
    public interface IAppConfigSettings
    {
        string SchedulerApiUrl { get; }

        string Username { get; }

        string Password { get; }
    }
}