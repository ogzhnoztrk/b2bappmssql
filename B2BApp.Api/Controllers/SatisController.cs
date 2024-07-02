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

    public class SatisController : ControllerBase
    {
        private readonly ISatisService _satisService;

        public SatisController(ISatisService satisService)
        {
            _satisService = satisService;
        }


        [HttpPost]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Satis> PostSatis(Satis satis)
        {

            _satisService.addSatis(satis);
            return new Result<Satis>
            {
                Data = satis,
                Message = "Satis başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
       
        [HttpGet]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Satis> GetSatis(string id)
        {

            var satis = _satisService.getSatisById(ObjectId.Parse(id));
            return satis;
        }

        [HttpGet("all")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<Satis>> GetSatis()
        {
            var satislar = _satisService.getAll();
            return satislar;
        }

        [HttpGet("getWithUrunAndSube")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SatisDto> getWithUrunAndSube(string id)
        {
            return _satisService.getWithUrunAndSube(ObjectId.Parse(id));
        }

        [HttpGet("getAllWithUrunAndSube")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SatisDto>> getAllWithUrunAndSube()
        {
            return _satisService.getAllWithUrunAndSube();
        }

        [HttpGet("GetAllWithUrunAndSubeByTedarikciId")] //Kullanıcı ulaşabilir
        public Result<ICollection<SatisDto>> getAllWithUrunAndSubeByTedarikciId(string tedarikciId)
        {
            return _satisService.getAllWithUrunAndSubeByTedarikciId(tedarikciId);
        }


        [HttpPut]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<Satis> UpdateSatis(Satis satis,  string satisId)
        {
            _satisService.updateSatis(satis, satisId);
            return new Result<Satis>
            {
                Data = satis,
                Message = "Satis başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Satis> DeleteSatis(string id)
        {
            _satisService.deleteSatis(ObjectId.Parse(id));

            return new Result<Satis>
            {
                Message = "Satis başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
