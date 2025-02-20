﻿using B2BApp.Models;
using B2BApp.Web.Core.Controllers;
using B2BApp.Web.Helpers.HttpHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;




namespace B2BApp.Web.Controllers
{
    public class AccountController : BaseController
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
                else
                {
                    Response.Cookies.Delete("jwt");
                }
            }



            return View();
        }
        public async Task<IActionResult> LoginPost(LoginVm loginVm)
        {

            //HttpClientHandler handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            //HttpClient client = new HttpClient(handler);

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44369/api/Auth/login");

            //request.Headers.Add("ac cept", "text/plain");

            //request.Content = new StringContent("{\n  \"kullaniciAdi\": \"" + loginVm.KullaniciAdi + "\",\n  \"sifre\": \"" + loginVm.Sifre + "\"\n}");
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //HttpResponseMessage response = await client.SendAsync(request);
            //// response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();

            //// JSON string'ini ayrıştır
            //var jsonObject = JObject.Parse(responseBody);

            try
            {
                var result = HttpService.Request<string, LoginVm>(null, HttpType.Post, "Auth/login", loginVm);


                if (!(result.Data is null))
                {
                    if (result.StatusCode.ToString() == "200")
                    {
                        // "data" alanını al
                        string data = result.Data.ToString();

                        Response.Cookies.Append("jwt", data, new CookieOptions
                        {
                            HttpOnly = true, // Cookie'ye JavaScript erişimini engeller
                            Secure = true,   // Sadece HTTPS üzerinden iletilir
                            SameSite = SameSiteMode.Strict, // CSRF saldırılarını önlemek için güçlendirilmiş güvenlik
                            Expires = DateTime.UtcNow.AddHours(6) // Cookie'nin son kullanma tarihi 

                        });
                        return RedirectToAction("login");

                    }
                }

                return RedirectToAction("login");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult SaveUser()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
           // if (!(ViewBag.FirmaId as string).Contains("6682972f420b0208d3d620a7")) return RedirectToAction("index", "home");

            return View();
        }

        public IActionResult LogoutAsync()
        {
            Response.Cookies.Delete("jwt");

            //_logger.LogInformation("user signout");

            return RedirectToAction("Login");
        }


    }
}
