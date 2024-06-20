using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SubeStokController : Controller
    {
        // GET: SubeStokController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubeStokController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubeStokController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubeStokController/Create
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

        // GET: SubeStokController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubeStokController/Edit/5
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

        // GET: SubeStokController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubeStokController/Delete/5
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
