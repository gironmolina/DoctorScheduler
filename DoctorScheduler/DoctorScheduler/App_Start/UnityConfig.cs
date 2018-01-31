using DoctorScheduler.Application.Services;
using DoctorScheduler.Domain.Services;
using System;
using Unity;
using DoctorScheduler.Application.Interfaces;
using DoctorScheduler.CrossCutting.Interfaces;
using DoctorScheduler.CrossCutting.Helpers;
using DoctorScheduler.Domain.Interfaces;
using DoctorScheduler.Infrastructure.Interfaces;
using DoctorScheduler.Infrastructure.Repositories;

namespace DoctorScheduler.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            // Application Services
            container.RegisterType<ISchedulerAppService, SchedulerAppService>();

            // Domain Services
            container.RegisterType<ISchedulerService, SchedulerService>();

            // Repositories
            container.RegisterType<ISchedulerRepository, SchedulerRepository>();

            // Cross-cutting
            container.RegisterType<IAppConfigSettings, AppConfigSettings>();
        }
    }
}