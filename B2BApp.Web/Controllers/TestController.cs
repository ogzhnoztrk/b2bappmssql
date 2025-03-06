using B2BApp.Entities.Concrete;
using B2BApp.Web.Helpers.HttpHelper;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            //get çalıştı
            //var result  = HttpService.Request<List<Kategori>>("", HttpType.Get, "kategori/all");

            //post çalıştı
            //var result = HttpService.Request<Kategori, Kategori>("", HttpType.Post, "Kategori" , new Kategori
            //{
            //    Id = Guid.NewGuid(),
            //    KategoriAdi = "Test  Kategorisi"
            //});

            //update çalıştı
            //var result = HttpService.Request<Kategori, Kategori>("", HttpType.Put, "Kategori?kategoriId=" + "755EC772-1663-433B-A59A-500649D448BA", new Kategori
            //{
            //    Id = Guid.Empty,
            //    KategoriAdi = "Test Kategorisi Guncellendi"

            //});
            
            //delete çalıştı
            //var result = HttpService.Request<Kategori>("", HttpType.Delete, "Kategori?id=755EC772-1663-433B-A59A-500649D448BA");

            return View();
        }
        public IActionResult testpage()
        {
            return View();
        }
    }
}
