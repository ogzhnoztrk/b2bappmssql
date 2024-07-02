using B2BApp.DTOs;
using B2BApp.Web.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Web.Controllers
{
    public class KategoriController : BaseController
    {
        // GET: KategoriController
        public ActionResult Index()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");

            return View();
        }




    }
}
