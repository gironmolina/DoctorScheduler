using DoctorScheduler.API;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Domain.Services;
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
            RegisterDependecies();
        }

        public static void RegisterDependecies()
        {
            Container = new UnityContainer();
            Container.RegisterType<ISchedulerService, SchedulerService>();
            Container.RegisterType<IAppConfigSettings, AppConfigSettings>();
        }

        public static void MockDependency<T>() where T : class 
        {
            Container.RegisterInstance(typeof(T), MockRepository.GenerateMock<T>());
        }
    }
}