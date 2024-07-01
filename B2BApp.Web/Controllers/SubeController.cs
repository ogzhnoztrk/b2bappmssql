using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SubeController : Controller
    {
        // GET: SubeController
        public ActionResult Index()
        {
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            ViewBag.JwtCookie = Request.Cookies["jwt"];

            return View();
        }

       



       
    }
}
