using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class SubeController : Controller
    {
        // GET: SubeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubeController/Create
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

        // GET: SubeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubeController/Edit/5
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

        // GET: SubeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubeController/Delete/5
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
