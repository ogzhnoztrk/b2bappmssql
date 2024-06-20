using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class UrunController : Controller
    {
        // GET: UrunController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UrunController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UrunController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UrunController/Create
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

        // GET: UrunController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UrunController/Edit/5
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

        // GET: UrunController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UrunController/Delete/5
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
