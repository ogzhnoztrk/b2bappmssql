using B2BApp.Web.Core.Controllers;
using B2BApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace B2BApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // JWT'yi çözme
            if (Request.Cookies["jwt"] == null) return RedirectToAction("login", "Account");

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
