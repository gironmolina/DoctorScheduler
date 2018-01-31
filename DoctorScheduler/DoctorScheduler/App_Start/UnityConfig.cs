using DoctorScheduler.Application.Services;
using DoctorScheduler.Domain.Services;
using System;
using DoctorScheduler.Application.Interfaces;
using Unity;

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
        }
    }
}