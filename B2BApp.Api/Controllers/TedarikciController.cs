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
    public class TedarikciController : ControllerBase
    {
        private readonly ITedarikciService _tedarikciService;

        public TedarikciController(ITedarikciService tedarikciService)
        {
                _tedarikciService = tedarikciService;
        }


        [HttpPost]
        public Result<Tedarikci> PostCompany(Tedarikci tedarikci)
        {

            _tedarikciService.addTedarikci(tedarikci);
            return new Result<Tedarikci>
            {
                Data = tedarikci,
                Message = "Tedarikci başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        public Result<Tedarikci> GetCompany(string id)
        {
            
            var tedarikci = _tedarikciService.getTedarikciById(ObjectId.Parse(id));
            return tedarikci;
        }
        [HttpGet("all")]
        public Result<ICollection<Tedarikci>> GetCompany()
        {
            var tedarikcilar = _tedarikciService.getAll();
            return tedarikcilar;
        }


        [HttpPut]
        public Result<Tedarikci> UpdateCompany(Tedarikci tedarikci,string tedarikciId)
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
        public Result<Tedarikci> DeleteCompany(string id)
        {
            _tedarikciService.deleteTedarikci(ObjectId.Parse(id));

            return new Result<Tedarikci>
            {
                Message = "Tedarikci başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
