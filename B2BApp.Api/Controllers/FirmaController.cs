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

    public class FirmaController : ControllerBase
    {
        private readonly IFirmaService _firmaService;

        public FirmaController(IFirmaService firmaService)
        {
                _firmaService = firmaService;
        }


        [HttpPost]
        public Result<Firma> PostFirma(Firma firma)
        {

            _firmaService.addFirma(firma);
            return new Result<Firma>
            {
                Data = firma,
                Message = "Firma başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Firma> GetFirma(string id)
        {
            
            var firma = _firmaService.getFirmaById(ObjectId.Parse(id));
            return firma;
        }

        [HttpGet("all")]
        public Result<ICollection<Firma>> GetFirma()
        {
            var firmalar = _firmaService.getAll();
            return firmalar;
        }


        [HttpPut]
        public Result<Firma> UpdateFirma(Firma firma,string firmaId)
        {
            //Firma firma = new Firma
            //{
            //    FirmaAdi = firmaDto.FirmaAdi,
            //    Id = ObjectId.Parse(firmaId)
            //};


            _firmaService.updateFirma(firma, firmaId);
            return new Result<Firma>
            {
                Data = firma,
                Message = "Firma başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        public Result<Firma> DeleteFirma(string id)
        {
            _firmaService.deleteFirma(ObjectId.Parse(id));

            return new Result<Firma>
            {
                Message = "Firma başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
