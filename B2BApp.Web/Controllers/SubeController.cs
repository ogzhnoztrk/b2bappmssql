using B2BApp.Web.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SubeController : BaseController
    {
        // GET: SubeController
        public ActionResult Index()
        {
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            if (!(ViewBag.FirmaId as string).Contains("6682972f420b0208d3d620a7")) return RedirectToAction("index", "home");

            return View();
        }






    }
}
