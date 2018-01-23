using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DoctorScheduler.Infrastucture.Helpers
{
    public static class HttpClientHelpers
    {
        private static readonly string username = ConfigurationManager.AppSettings["Username"];
        private static readonly string password = ConfigurationManager.AppSettings["Password"];

        public static async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                using (var response = await client.GetAsync(url))
                {
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false));
                    return json.ToObject<T>();
                }
            }
        }
    }
}
