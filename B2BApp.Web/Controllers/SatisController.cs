using B2BApp.Web.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace B2BApp.Web.Controllers
{
    public class SatisController : BaseController
    {
        // GET: SatisController
        public ActionResult Index()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            return View();
        }

           // GET: SatisController
        public ActionResult SatisRapor()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            

            return View();
        }
               public ActionResult DetayliSatisRapor()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");
            

            return View();
        }

       



       
    }
}
