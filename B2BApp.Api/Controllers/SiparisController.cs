using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public Result<ICollection<Siparis>> GetSiparis()
        {
            var siparislar = _siparisService.getAll();
            return siparislar;
        }
      
        [HttpGet("GetAllWithDetails")]
        public Result<ICollection<SiparisDto>> GetAllWithDetails()
        {
            return _siparisService.getAllWithDetails();
        }
       
        [HttpGet("GetAllWithDetailsById")]
        public Result<SiparisDto> GetAllWithDetailsById(string siparisId)
        {
            return _siparisService.getAllWithDetailsById(siparisId);
        }
       
        [HttpPut]
        public Result<Siparis> UpdateSiparis(Siparis siparis,string siparisId)
        {
            _siparisService.updateSiparis(siparis, siparisId);
            return new Result<Siparis>
            {
                Data = siparis,
                Message = "Siparis başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
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
