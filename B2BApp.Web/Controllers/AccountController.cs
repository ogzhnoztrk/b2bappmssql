using B2BApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;




namespace B2BApp.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            //çerezler içerisinde jwt var mı kontrol et
            if (Request.Cookies["jwt"] != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = (handler.ReadToken(Request.Cookies["jwt"]) as JwtSecurityToken);
                var exp = jsonToken.Claims.First(q => q.Type.Equals("exp")).Value;
                var ticks = long.Parse(exp);
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
                var now = DateTime.Now.ToUniversalTime();
                // JWT'yi çözme eğer jwt suresi dolmamış ise direk yonlendir
                if ((tokenDate >= now))
                {
                    return RedirectToAction("Index", "Home");
                }
            }



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
            
            

            if (jsonObject["statusCode"].ToString() != "400")
            {
                // "data" alanını al
                string data = jsonObject["data"].ToString();

                Response.Cookies.Append("jwt", data, new CookieOptions
                {
                    HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                    Secure = true,   // Sadece HTTPS üzerinden iletilir
                    SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                    Expires = DateTime.UtcNow.AddDays(1) // Cookie'nin son kullanma tarihi 

                });
                return RedirectToAction("login");

            }

            return RedirectToAction("login");

        }



    }
}
