using B2BApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;




namespace B2BApp.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {




            return View();
        }
        public async Task<IActionResult> LoginPost(LoginVm loginVm) 
        {
            


            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost/api/Auth/login");

            request.Headers.Add("accept", "text/plain");

            request.Content = new StringContent("{\n  \"kullaniciAdi\": \"" + loginVm.KullaniciAdi+"\",\n  \"sifre\": \""+loginVm.Sifre+"\"\n}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.SendAsync(request);
           // response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            // JSON string'ini ayrıştır
            var jsonObject = JObject.Parse(responseBody);

            // "data" alanını al
            string data = jsonObject["data"].ToString();

            Response.Cookies.Append("jwt", data, new CookieOptions
            {
                HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                Secure = true,   // Sadece HTTPS üzerinden iletilir
                SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                Expires = DateTime.UtcNow.AddDays(1) // Cookie'nin son kullanma tarihi 
            });


            return Ok();
        }



    }
}
