using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class SubeStokController : ControllerBase
    {
        private readonly ISubeStokService _subeStokService;

        public SubeStokController(ISubeStokService subeStokService)
        {
            _subeStokService = subeStokService;
        }

        [HttpPost("InsertMany")]
       // [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<List<SubeStok>> addManySubeStok(List<SubeStok> subeStok)
        {
            _subeStokService.addManySubeStok(subeStok);
            return new Result<List<SubeStok>>
            {
                Data = subeStok,
                Message = "SubeStok başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpPost]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SubeStok> PostSubeStok(SubeStok subeStok)
        {

            _subeStokService.addSubeStok(subeStok);
            return new Result<SubeStok>
            {
                Data = subeStok,
                Message = "SubeStok başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpGet]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SubeStok> GetSubeStok(string id)
        {

            var subeStok = _subeStokService.getSubeStokById(ObjectId.Parse(id));
            return subeStok;
        }

        [HttpGet("all")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SubeStok>> GetSubeStok()
        {
            var subeStoklar = _subeStokService.getAll();
            return subeStoklar;
        }

        [HttpGet("GetWithSubeAndUrun")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SubeStokDto> GetWithSubeAndUrun(string id)
        {
            return _subeStokService.getWithSubeAndUrun(ObjectId.Parse(id));
        }

        [HttpGet("GetAllWithSubeAndUrunByTedarikciId")] //kullanıcılar ulaşabilir
        public Result<ICollection<SubeStokDto>> GetAllWithSubeAndUrunByTedarikciId(
            string tedarikciId,
            string? subeId,
            string? firmaId,
            string? kategoriId
            )
        {
            return _subeStokService.getAllWithSubeAndUrunByTedarikciId(tedarikciId, subeId, firmaId, kategoriId);
        }

        [HttpGet("GetAllWithDetailsByFilters")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SubeStokDto>> getAllWithDetailsByFilters(string? subeId, string? firmaId, string? kategoriId)
        {
            return _subeStokService.getAllWithDetailsByFilters(subeId, firmaId, kategoriId);
        }

        [HttpGet("GetAllWithSubeAndUrun")]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SubeStokDto>> GetAllWithSubeAndUrun()
        {
            return _subeStokService.getAllWithSubeAndUrun();
        }


        [HttpPut]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SubeStok> UpdateSubeStok(SubeStok subeStok, string subeStokId)
        {

            _subeStokService.updateSubeStok(subeStok, subeStokId);
            return new Result<SubeStok>
            {
                Data = subeStok,
                Message = "SubeStok başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SubeStok> DeleteSubeStok(string id)
        {
            _subeStokService.deleteSubeStok(ObjectId.Parse(id));

            return new Result<SubeStok>
            {
                Message = "SubeStok başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
