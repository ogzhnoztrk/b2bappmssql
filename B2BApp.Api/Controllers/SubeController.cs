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
    public class SubeController : ControllerBase
    {

        private readonly ISubeService _subeService;

        public SubeController(ISubeService subeService)
        {
            _subeService = subeService;
        }


        [HttpPost]
        public Result<Sube> PostSube(Sube sube)
        {

            _subeService.addSube(sube);
            return new Result<Sube>
            {
                Data = sube,
                Message = "Sube başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Sube> GetSube(string id)
        {

            var sube = _subeService.getSubeById(ObjectId.Parse(id));
            return sube;
        }
        [HttpGet("all")]
        public Result<ICollection<Sube>> GetSube()
        {
            var subelar = _subeService.getAll();
            return subelar;
        }

        [HttpGet("GetSubelerWithFirma")]
        public Result<ICollection<SubeDto>> GetSubelerWithFirma()
        {
            return _subeService.getSubelerWithFirma();
        }

        [HttpGet("GetSubeWithFirma")]
        public Result<SubeDto> GetSubeWithFirma(string id)
        {
            return _subeService.getSubeWithFirma(ObjectId.Parse(id));
        }


        [HttpPut]
        public Result<Sube> UpdateSube(Sube sube, string subeId)
        {

            //Sube sube = new Sube
            //{
            //    Id = ObjectId.Parse(subeDto.ObjectId.ToString()),
            //    FirmaId = ObjectId.Parse(subeDto.FirmaId.ToString()),
            //    SubeAdi = subeDto.SubeAdi,
            //    SubeTel = subeDto.SubeTel,
                
            //};


            _subeService.updateSube(sube, subeId);
            return new Result<Sube>
            {
                Data = sube,
                Message = "Sube başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        public Result<Sube> DeleteSube(string id)
        {
            _subeService.deleteSube(ObjectId.Parse(id));

            return new Result<Sube>
            {
                Message = "Sube başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }


    }
}
