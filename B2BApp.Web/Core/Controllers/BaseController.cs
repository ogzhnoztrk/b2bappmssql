﻿

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace B2BApp.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // JWT'yi dondormek icin
            if (Request.Cookies["jwt"] != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(Request.Cookies["jwt"]) as JwtSecurityToken;

                // Claims JSON olarak okuma
                var claimsJson = new JObject();
                foreach (var claim in jsonToken.Claims)
                {
                    claimsJson.Add(claim.Type, claim.Value);
                }

                ViewBag.FirmaId = claimsJson["role"].ToString();
                ViewBag.KullanıcıAdi = claimsJson["unique_name"].ToString();
                ViewBag.JwtCookie = Request.Cookies["jwt"];
            }


        }
    }

}
