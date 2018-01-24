using System.Web.Http;
using WebActivatorEx;
using DoctorScheduler.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DoctorScheduler.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Doctor Scheduler API");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\DoctorScheduler.API.XML",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi();
        }
    }
}
