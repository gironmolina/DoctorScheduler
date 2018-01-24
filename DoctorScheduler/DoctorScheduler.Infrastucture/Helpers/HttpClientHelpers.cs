using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DoctorScheduler.Infrastucture.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DoctorScheduler.Infrastucture.Helpers
{
    public static class HttpClientHelpers
    {
        private static readonly string Username = ConfigurationManager.AppSettings["Username"];
        private static readonly string Password = ConfigurationManager.AppSettings["Password"];

        public static async Task<T> GetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                // Workaround to avoid SSL certificate verification
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                using (var response = await client.GetAsync(url).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new SchedulerBadRequestException();
                    }

                    var json = JObject.Parse(await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false));
                    return json.ToObject<T>();
                }
            }
        }

        public static async Task<bool> PostAsync(string url, object content)
        {
            using (var client = new HttpClient())
            {
                // Workaround to avoid SSL certificate verification
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                var postBody = JsonConvert.SerializeObject(content);
                var stringContent = new StringContent(postBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, stringContent).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
