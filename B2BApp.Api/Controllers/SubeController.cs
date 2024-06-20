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
        public Result<Sube> PostCompany(Sube sube)
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
        public Result<Sube> GetCompany(string id)
        {

            var sube = _subeService.getSubeById(ObjectId.Parse(id));
            return sube;
        }
        [HttpGet("all")]
        public Result<ICollection<Sube>> GetCompany()
        {
            var subelar = _subeService.getAll();
            return subelar;
        }



        [HttpPut]
        public Result<Sube> UpdateCompany(SubeDto subeDto, string subeId)
        {

            Sube sube = new Sube
            {
                Id = ObjectId.Parse(subeDto.ObjectId.ToString()),
                FirmaId = ObjectId.Parse(subeDto.FirmaId.ToString()),
                SubeAdi = subeDto.SubeAdi,
                SubeTel = subeDto.SubeTel,
                
            };


            _subeService.updateSube(sube, subeId);
            return new Result<Sube>
            {
                Data = sube,
                Message = "Sube başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        public Result<Sube> DeleteCompany(string id)
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
