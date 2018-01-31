using DoctorScheduler.Application.Interfaces;
using DoctorScheduler.Application.Services;
using DoctorScheduler.API;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Domain.Services;
using NUnit.Framework;
using Unity;
using DoctorScheduler.Infrastucture.Interfaces;
using DoctorScheduler.Infrastucture.Repositories;

namespace DoctorScheduler.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        public static IUnityContainer Container { get; private set; }

        [OneTimeSetUp]
        public static void ConfigureDependecies()
        {
            AutoMapperConfig.RegisterMappings();
            Container = new UnityContainer();
            Container.RegisterType<ISchedulerAppService, SchedulerAppService>();
            Container.RegisterType<ISchedulerService, SchedulerService>();
            Container.RegisterType<ISchedulerRepository, SchedulerRepository>();
            Container.RegisterType<IAppConfigSettings, AppConfigSettings>();
        }
    }
}
