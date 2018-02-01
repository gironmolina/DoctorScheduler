using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DoctorScheduler.CrossCutting.Helpers
{
    public static class HttpClientHelpers
    {
        public static async Task<T> GetAsync<T>(string url, string username, string password)
        {
            using (var client = new HttpClient())
            {
                // Workaround to avoid SSL certificate verification
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));

                using (var response = await client.GetAsync(url).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    var json = JObject.Parse(await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false));
                    return json.ToObject<T>();
                }
            }
        }

        public static async Task<bool> PostAsync(string url, string username, string password, object content)
        {
            using (var client = new HttpClient())
            {
                // Workaround to avoid SSL certificate verification
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
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