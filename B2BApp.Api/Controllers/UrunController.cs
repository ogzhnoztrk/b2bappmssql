using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "")]
    //[Authorize]

    public class UrunController : ControllerBase
    {

        private readonly IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }


        [HttpPost]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Urun> PostUrun(Urun urun)
        {
            //urun.Id = Guid.NewGuid();
            _urunService.addUrun(urun);
            return new Result<Urun>
            {
                Data = urun,
                Message = "Urun başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpGet]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Urun> GetUrun(string id)
        {

            var urun = _urunService.getUrunById(    Guid.Parse(id));
            return urun;
        }

        [HttpGet("GetUrunlerWithKategori")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<UrunDto>> GetUrunWithKategori()
        {
            return _urunService.getAllWithKategoriAdiAndTedarikci();


        }

        [HttpGet("GetUrunlerWithDetailsByTedarikciId")] //Kullanıcı ulaşabilir
        public Result<ICollection<UrunDto>> GetUrunlerWithDetailsByTedarikciId(string tedarikciId)
        {
            return _urunService.getUrunlerWithDetailsByTedarikciId(tedarikciId);


        }

        [HttpGet("GetUrunWithKategori")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<UrunDto> GetUrunWithKategori(string id)
        {
            return _urunService.getUrunWithKategoriAndTedarikci(Guid.Parse(id));
        }

        [HttpGet("all")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<Urun>> GetUrun()
        {
            var urunlar = _urunService.getAll();
            return urunlar;
        }

        [HttpPut]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Urun> UpdateUrun(Urun urun, string urunId)
        {
            _urunService.updateUrun(urun, urunId);
            return new Result<Urun>
            {
                Data = urun,
                Message = "Urun başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Urun> DeleteUrun(string id)
        {
            _urunService.deleteUrun(Guid.Parse(id));

            return new Result<Urun>
            {
                Message = "Urun başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }




    }
}
