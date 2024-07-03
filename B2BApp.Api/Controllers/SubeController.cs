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

    public class SubeController : ControllerBase
    {

        private readonly ISubeService _subeService;

        public SubeController(ISubeService subeService)
        {
            _subeService = subeService;
        }


        [HttpPost]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
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
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Sube> GetSube(string id)
        {

            var sube = _subeService.getSubeById(ObjectId.Parse(id));
            return sube;
        }
        [HttpGet("all")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<ICollection<Sube>> GetAllSube(string? firmaId)
        {
            var subelar = _subeService.getAll(firmaId);
            return subelar;
        }

        [HttpGet("GetSubelerByFirmaId")]
        public Result<ICollection<Sube>> getSubelerByFirmaId(string subeId)

        {
            return _subeService.getSubeByFirmaId(subeId);
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
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Sube> UpdateSube(Sube sube, string subeId)
        {

            _subeService.updateSube(sube, subeId);
            return new Result<Sube>
            {
                Data = sube,
                Message = "Sube başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
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
