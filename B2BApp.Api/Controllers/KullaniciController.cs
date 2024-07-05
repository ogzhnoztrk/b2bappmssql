using B2BApp.Business.Abstract;
using B2BApp.Business.Concrete;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "6682972f420b0208d3d620a7")]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;
        public KullaniciController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [HttpPost]
        public Result<Kullanici> PostKullanici(Kullanici kullanici)
        {

            _kullaniciService.addKullanici(kullanici);
            return new Result<Kullanici>
            {
                Data = kullanici,
                Message = "Kullanici başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Kullanici> GetKullanici(string id)
        {

            var kullanici = _kullaniciService.getKullaniciById(ObjectId.Parse(id));
            return kullanici;
        }
        [HttpGet("all")]
        public Result<ICollection<Kullanici>> GetKullanici()
        {
            var kullanicilar = _kullaniciService.getAll();
            return kullanicilar;
        }
        [HttpGet("getAllWithTedarikci")]
        public Result<ICollection<KullaniciDto>> getAllWithTedarikci()
        {
            return _kullaniciService.getAllWithTedarikci();
        }


        [HttpPut]
        public Result<Kullanici> UpdateKullanici(Kullanici kullanici, string kullaniciId)
        {

            _kullaniciService.updateKullanici(kullanici, kullaniciId);
            return new Result<Kullanici>
            {
                Data = kullanici,
                Message = "Kullanici başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        public Result<Kullanici> DeleteKullanici(string id)
        {
            _kullaniciService.deleteKullanici(ObjectId.Parse(id));

            return new Result<Kullanici>
            {
                Message = "Kullanici başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }

    }
}
