using DoctorScheduler.API;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Domain.Services;
using DoctorScheduler.Infrastructure.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;
using Unity;

namespace DoctorScheduler.Domain.Tests
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
            Container.RegisterType<ISchedulerService, SchedulerService>();
            Container.RegisterType<IAppConfigSettings, AppConfigSettings>();

            // Mocks
            Container.RegisterInstance(typeof(ISchedulerRepository), MockRepository.GenerateMock<ISchedulerRepository>());
        }
    }
}