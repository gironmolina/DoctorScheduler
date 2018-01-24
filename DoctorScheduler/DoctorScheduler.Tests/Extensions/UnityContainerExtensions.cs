using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Unity;

namespace DoctorScheduler.Tests.Extensions
{
    public static class UnityContainerExtensions
    {
        public static T GetController<T>(this IUnityContainer container) where T : ApiController
        {
            var sut = container.Resolve<T>();
            sut.Request = new HttpRequestMessage();
            sut.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return sut;
        }
    }
}
