using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SatisController : Controller
    {
        // GET: SatisController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SatisController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SatisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SatisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SatisController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SatisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SatisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SatisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




       
    }
}
