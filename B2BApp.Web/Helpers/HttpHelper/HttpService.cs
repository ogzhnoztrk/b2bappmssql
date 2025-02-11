using Core.Models.Concrete;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace B2BApp.Web.Helpers.HttpHelper
{
    //Http isteklerini yapar ve geriye Result tipinde veri döner. 
    public static class HttpService
    {

        public static string ApiLink { get; internal set; }


        //Api linkini endpoint kısmına kadar oluşturur
        public static string _apiLinkGenerate(string ip, int port, bool useSsl, string startpath = "api"/*, string endpoint = ""*/)
        {
            var link = string.Format("{0}://{1}:{2}/{3}", (object)(useSsl ? "https" : "http"), (object)ip, (object)port, (object)startpath/*, (object)endpoint*/);
            return link;
        }



        //T şeklinde gelen veri tipine göre istek yapar ve geriye Result tipinde veri döner.
        public static Result<T> Request<T>(string token, HttpType type, string endpoint, string body = "")
        {
            Result<T> response;

            try
            {

                var requestLink = string.Format("{0}/{1}", ApiLink, endpoint);


                var client = new HttpClient
                {
                    BaseAddress = new Uri(requestLink),
                    MaxResponseContentBufferSize = Int32.MaxValue
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }

                Task<HttpResponseMessage> task;
                switch (type)
                {
                    case HttpType.Post:
                       
                        var a = new StringContent(body, Encoding.UTF8, "application/json");
                        task = client.PostAsync(requestLink, content: new StringContent(body));
                        break;
                    case HttpType.Put:
                        
                        task = client.PutAsync(requestLink, content: new StringContent(body));
                        break;
                    case HttpType.Delete:
                        task = client.DeleteAsync(requestLink);
                        break;
                    default:
                        task = client.GetAsync(requestLink);
                        break;
                }
                task.Wait();

                using (HttpResponseMessage _response = task.Result)
                {
                    _response.EnsureSuccessStatusCode();

                    Task<string> taskResponse = _response.Content.ReadAsStringAsync();
                    taskResponse.Wait();

                    //string responseString = taskResponse.Result;
                    //if (string.IsNullOrEmpty(responseString))
                    //{
                    //    throw new Exception("");
                    //}

                    response = JsonConvert.DeserializeObject<Result<T>>(taskResponse.Result);
                }
            }
            catch (Exception ex)
            {
                response = new Result<T>();
            }

            return response;
        }
    }
}
/// <summary>
/// Http istek tipi
/// </summary>
/// <example>Get, Post, Put, Delete</example>
public enum HttpType
{
    Get,
    Post,
    Put,
    Delete
}