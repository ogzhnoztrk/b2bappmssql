using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {

        private readonly IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }


        [HttpPost]
        public Result<Urun> PostCompany(Urun urun)
        {

            _urunService.addUrun(urun);
            return new Result<Urun>
            {
                Data = urun,
                Message = "Urun başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Urun> GetCompany(string id)
        {

            var urun = _urunService.getUrunById(ObjectId.Parse(id));
            return urun;
        }
        [HttpGet("all")]
        public Result<ICollection<Urun>> GetCompany()
        {
            var urunlar = _urunService.getAll();
            return urunlar;
        }


        [HttpPut]
        public Result<Urun> UpdateCompany(UrunDto urunDto,string urunId)
        {

            Urun urun = new Urun
            {
                Id = ObjectId.Parse(urunDto.BaseObjectId.ToString()),
                KategoriId = ObjectId.Parse(urunDto.KategoriId.ToString()),
                Fiyat = urunDto.Fiyat,
                UrunAdi = urunDto.UrunAdi,
            };


            _urunService.updateUrun(urun, urunId);
            return new Result<Urun>
            {
                Data = urun,
                Message = "Urun başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        public Result<Urun> DeleteCompany(string id)
        {
            _urunService.deleteUrun(ObjectId.Parse(id));

            return new Result<Urun>
            {
                Message = "Urun başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }

    }
}
