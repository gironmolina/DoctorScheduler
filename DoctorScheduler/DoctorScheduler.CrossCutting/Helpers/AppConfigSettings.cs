using System.Configuration;
using DoctorScheduler.CrossCutting.Interfaces;

namespace DoctorScheduler.CrossCutting.Helpers
{
    public class AppConfigSettings : IAppConfigSettings
    {
        public string SchedulerApiUrl { get; }

        public string Username { get; }

        public string Password { get; }

        public AppConfigSettings()
        {
            this.SchedulerApiUrl = ConfigurationManager.AppSettings["SchedulerAPIUrl"];
            this.Username = ConfigurationManager.AppSettings["Username"];
            this.Password = ConfigurationManager.AppSettings["Password"];
        }
    }
}
