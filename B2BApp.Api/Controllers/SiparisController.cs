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
    [Authorize]
    public class SiparisController : ControllerBase
    {
        private readonly ISiparisService _siparisService;

        public SiparisController(ISiparisService siparisService)
        {
            _siparisService = siparisService;
        }


        [HttpPost]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Siparis> PostSiparis(Siparis siparis)
        {

            _siparisService.addSiparis(siparis);
            return new Result<Siparis>
            {
                Data = siparis,
                Message = "Siparis başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Siparis> GetSiparis(string id)
        {

            var siparis = _siparisService.getSiparisById(ObjectId.Parse(id));
            return siparis;
        }

        [HttpGet("all")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<Siparis>> GetSiparis()
        {
            var siparislar = _siparisService.getAll();
            return siparislar;
        }

        [HttpGet("GetAllWithDetails")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SiparisDto>> GetAllWithDetails()
        {
            return _siparisService.getAllWithDetails();
        }

        [HttpGet("GetAllWithDetailsById")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SiparisDto> GetAllWithDetailsById(string siparisId)
        {
            return _siparisService.getAllWithDetailsById(siparisId);
        }

        [HttpGet("GetAllWithDetailsByFilters")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SiparisDto>> GetAllWithDetailsByFilters(DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi)
        {
            return _siparisService.getAllWithDetailsByFilters(tarih1, tarih2, urunId, subeId, aktifMi);
        }

        [HttpGet("GetAllWithDetailsByFiltersAndTedarikciId")]
        public Result<ICollection<SiparisDto>> GetAllWithDetailsByFiltersAndTedarikciId(
             string tedarikciId, DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi)
        {
            return _siparisService.getAllWithDetailsByFiltersAndTedarikciId(tedarikciId, tarih1, tarih2, urunId, subeId, aktifMi);
        }

        [HttpPut]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Siparis> UpdateSiparis(Siparis siparis, string siparisId)
        {
            _siparisService.updateSiparis(siparis, siparisId);
            return new Result<Siparis>
            {
                Data = siparis,
                Message = "Siparis başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpPut("changeAktiflik")]
        public Result<string> changeAktiflik(string siparisId)
        {
            return _siparisService.changeAktiflik(siparisId);
        }
        [HttpDelete]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Siparis> DeleteSiparis(string id)
        {
            _siparisService.deleteSiparis(ObjectId.Parse(id));

            return new Result<Siparis>
            {
                Message = "Siparis başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
