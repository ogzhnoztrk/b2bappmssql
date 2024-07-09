using B2BApp.Web.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SubeStokController : BaseController
    {
        // GET: SubeStokController
        public ActionResult Index()
        {
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");


            return View();
        }

        // GET: SubeStokController
        public ActionResult SubeStokDurum()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");

            return View();
        }

        public ActionResult StokRapor()
        {

            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");

            return View();
        }
    }





}

