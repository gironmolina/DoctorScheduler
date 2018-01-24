using System.Web.Http;
using DoctorScheduler.API.Filter;

namespace DoctorScheduler.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.RegisterMappings();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ValidateModelStateAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new CheckModelForNullAttribute());
        }
    }
}
