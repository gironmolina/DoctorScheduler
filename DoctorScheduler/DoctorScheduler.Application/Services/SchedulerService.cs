using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DoctorScheduler.Application.Services
{
    public class SchedulerService : ISchedulerService
    {
        public async Task<dynamic> GetWeeklyAvailability()
        {
            const string url = "https://test.draliacloud.net/api/availability/GetWeeklyAvailability/20180101";
            
            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes("techuser:secretpassWord");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(byteArray));

                using (var response = await client.GetAsync(url))
                { 
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                    return json.ToObject<dynamic>();
                }
            }
        }
    }
}
