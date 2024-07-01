using B2BApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Web.Controllers
{
    public class KategoriController : Controller
    {
        // GET: KategoriController
        public ActionResult Index()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            ViewBag.JwtCookie = Request.Cookies["jwt"];

            return View();
        }




    }
}
