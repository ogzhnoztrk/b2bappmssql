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


        public static Result<TResponse> Request<TResponse>(string? token, HttpType type, string endpoint) 
        {
            return Request<TResponse, object>(token, type, endpoint, null);
        }

        //T şeklinde gelen veri tipine göre istek yapar ve geriye Result tipinde veri döner.
        public static Result<TResponse> Request<TResponse, TRequest>(string? token, HttpType type, string endpoint, TRequest? body) 
        {
            Result<TResponse> response;

            try
            {

                var requestLink = string.Format("{0}/{1}", ApiLink, endpoint);


                var client = new HttpClient
                {
                    BaseAddress = new Uri(requestLink),
                    MaxResponseContentBufferSize = Int32.MaxValue
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }


                string bodystr = JsonConvert.SerializeObject(body);//.Replace(" ", "") bu veri içerisni de değiştiriyor
                //bodystr = bodystr.Replace("\r\n", "").Replace("\r", "") ;

                Task<HttpResponseMessage> task;
                switch (type)
                {

                    case HttpType.Post:

                        //utf8 ve media type eklendi
                        task = client.PostAsync(requestLink, content: new StringContent(bodystr, Encoding.UTF8, "application/json"));
                        break;
                    case HttpType.Put:
                        //utf8 ve media type eklendi
                        task = client.PutAsync(requestLink, content: new StringContent(bodystr));
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

                    response = JsonConvert.DeserializeObject<Result<TResponse>>(taskResponse.Result);
                }
            }
            catch (Exception ex)
            {
                response = new Result<TResponse>(400, ex.Message);
            }

            return response;
        }


        //guncellenmiş hali post ve put kısmında veriyi T'ye çevirip multipartFormdatacontent ile gönderiyoruz
        //public static Result<TResponse> Request<TResponse>(string? token, HttpType type, string endpoint)
        //{
        //    return Request<object, TResponse>(token, type, endpoint, null);
        //}


        ///// <summary>
        ///// <typeparamref name="TRequest"/> post ve put işlemlerini yaparken yollanılan değer veya model <typeparamref name="TResponse"/> değerinde bir result döner. 
        ///// </summary>
        ///// <typeparam name="TRequest">Post-Put body kısmına yerleştirilecek olan değerler</typeparam>
        ///// <typeparam name="TResponse">Cevap tipi</typeparam>
        ///// <param name="token"></param>
        ///// <param name="type"></param>
        ///// <param name="endpoint"></param>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //public static Result<TResponse> Request<TRequest, TResponse>(string? token, HttpType type, string endpoint, TRequest? body)
        //{
        //    Result<TResponse> response;

        //    try
        //    {
        //        var requestLink = string.Format("{0}/{1}", ApiLink, endpoint);

        //        var client = new HttpClient
        //        {
        //            BaseAddress = new Uri(requestLink),
        //            MaxResponseContentBufferSize = Int32.MaxValue
        //        };

        //        if (!string.IsNullOrEmpty(token))
        //        {
        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //        }

        //        Task<HttpResponseMessage> task;
        //        switch (type)
        //        {
        //            case HttpType.Post:
        //                // Form içeriği oluştur
        //                var formContent = new MultipartFormDataContent();
        //                if (body != null)
        //                {
        //                    var properties = typeof(TRequest).GetProperties();
        //                    foreach (var prop in properties)
        //                    {
        //                        var value = prop.GetValue(body)?.ToString();
        //                        if (value != null)
        //                        {
        //                            formContent.Add(new StringContent(value), prop.Name);
        //                        }
        //                    }
        //                }
        //                task = client.PostAsync(requestLink, formContent);
        //                break;
        //            case HttpType.Put:
        //                // Put için de form içeriği oluştur
        //                var putFormContent = new MultipartFormDataContent();
        //                if (body != null)
        //                {
        //                    var properties = typeof(TRequest).GetProperties();
        //                    foreach (var prop in properties)
        //                    {
        //                        var value = prop.GetValue(body)?.ToString();
        //                        if (value != null)
        //                        {
        //                            putFormContent.Add(new StringContent(value), prop.Name);
        //                        }
        //                    }
        //                }
        //                task = client.PutAsync(requestLink, putFormContent);
        //                break;
        //            case HttpType.Delete:
        //                task = client.DeleteAsync(requestLink);
        //                break;
        //            default:
        //                task = client.GetAsync(requestLink);
        //                break;
        //        }
        //        task.Wait();

        //        using (HttpResponseMessage _response = task.Result)
        //        {
        //            _response.EnsureSuccessStatusCode();

        //            Task<string> taskResponse = _response.Content.ReadAsStringAsync();
        //            taskResponse.Wait();

        //            //string responseString = taskResponse.Result;
        //            //if (string.IsNullOrEmpty(responseString))
        //            //{
        //            //    throw new Exception("");
        //            //}

        //            response = JsonConvert.DeserializeObject<Result<TResponse>>(taskResponse.Result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = new Result<TResponse>();
        //    }

        //    return response;
        //}

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