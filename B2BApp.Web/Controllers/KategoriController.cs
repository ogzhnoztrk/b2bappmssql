using B2BApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Web.Controllers
{
    public class KategoriController : Controller
    {
        // GET: KategoriController
        public ActionResult Index()
        {
            return View();
        }




        #region
        [HttpDelete]
        public void Delete(ObjectId id)
        {

        }



        #endregion



    }
}
