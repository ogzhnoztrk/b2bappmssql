using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace B2BApp.Web.Services
{
    public class ApiService<T> : IApiService<T>
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<T> PostAsync(string url, T data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                using (HttpClient client = new HttpClient(handler))
                {
                    // Veriyi JSON formatına çevirme
                    string jsonData = System.Text.Json.JsonSerializer.Serialize(data);
                    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    var responseData = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<T>(responseData);
                }

            }
        }
        public async Task<T> PutAsync(string url, T data)
        {

            //var jsonData = JsonSerializer.Serialize(data);
            //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PutAsync(url, content);
            ////response.EnsureSuccessStatusCode();
            //var responseData = await response.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<T>(responseData);

            var jsonData = System.Text.Json.JsonSerializer.Serialize<T>(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            var responseData = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseData);
        }
        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public async Task<T> GetAsync(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (HttpClient client = new HttpClient(handler))
                {

                    var response = await client.GetAsync(url);
                    //response.EnsureSuccessStatusCode();
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseData); ;

                }

            }
        }


    }
}
