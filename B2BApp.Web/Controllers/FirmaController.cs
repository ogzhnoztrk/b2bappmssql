using B2BApp.Web.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class FirmaController : BaseController
    {
        // GET: KategoriController
        public ActionResult Index()
        {
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");


            return View();
        }

        



       
    }
}
