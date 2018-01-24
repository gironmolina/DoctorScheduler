using DoctorScheduler.Application.Services;
using DoctorScheduler.API;
using DoctorScheduler.Domain.Services;
using NUnit.Framework;
using Unity;

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
        }
    }
}
