using B2BApp.Business.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class TedarikciController : ControllerBase
    {
        private readonly ITedarikciService _tedarikciService;

        public TedarikciController(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;
        }


        [HttpPost]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Tedarikci> PostCompany(Tedarikci tedarikci)
        {
           // tedarikci.Id = Guid.NewGuid();

            _tedarikciService.addTedarikci(tedarikci);
            return new Result<Tedarikci>
            {
                Data = tedarikci,
                Message = "Tedarikci başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpGet]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Tedarikci> GetCompany(string id)
        {

            var tedarikci = _tedarikciService.getTedarikciById( Guid.Parse(id));
            return tedarikci;
        }

        [HttpGet("all")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<Tedarikci>> GetCompany()
        {
            var tedarikcilar = _tedarikciService.getAll();
            return tedarikcilar;
        }


        [HttpPut]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Tedarikci> UpdateCompany(Tedarikci tedarikci, string tedarikciId)
        {
            _tedarikciService.updateTedarikci(tedarikci, tedarikciId);
            return new Result<Tedarikci>
            {
                Data = tedarikci,
                Message = "Tedarikci başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Tedarikci> DeleteCompany(string id)
        {
            _tedarikciService.deleteTedarikci(Guid.Parse(id));

            return new Result<Tedarikci>
            {
                Message = "Tedarikci başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
