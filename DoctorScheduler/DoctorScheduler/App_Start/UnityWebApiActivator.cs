using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DoctorScheduler.API.UnityWebApiActivator), nameof(DoctorScheduler.API.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(DoctorScheduler.API.UnityWebApiActivator), nameof(DoctorScheduler.API.UnityWebApiActivator.Shutdown))]

namespace DoctorScheduler.API
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            var resolver = new UnityDependencyResolver(UnityConfig.Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}