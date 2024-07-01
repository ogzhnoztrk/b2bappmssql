using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class FirmaController : Controller
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
