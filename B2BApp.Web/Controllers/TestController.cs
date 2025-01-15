using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
