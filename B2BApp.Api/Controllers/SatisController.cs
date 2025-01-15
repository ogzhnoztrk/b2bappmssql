using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ////[Authorize]

    public class SatisController : ControllerBase
    {
        private readonly ISatisService _satisService;

        public SatisController(ISatisService satisService)
        {
            _satisService = satisService;
        }

        [HttpPost("InsertMany")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<List<Satis>> addManySatis(List<Satis> satislar)
        {
            //foreach (var item in collection)
            //{

            //}
            _satisService.addManySatis(satislar);
            return new Result<List<Satis>>
            {
                Data = satislar,
                Message = "Satislar başarıyla eklendi",
                
            };
        }

        [HttpPost]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
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
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Satis> GetSatis(string id)
        {

            var satis = _satisService.getSatisById(Int32.Parse(id));
            return satis;
        }

        [HttpGet("all")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<Satis>> GetSatis()
        {
            var satislar = _satisService.getAll();
            return satislar;

        }


        [HttpGet("getWithUrunAndSube")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<SatisDto> getWithUrunAndSube(string id)
        {
            return _satisService.getWithUrunAndSube(Int32.Parse(id));
        }

        [HttpGet("getAllWithUrunAndSube")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")] 
        public async Task<Result<object>> getAllWithUrunAndSube(
            int offset = 1,
            int limit = 10,
            string sort = "id",
            string order = "asc",
            string? urun = null,
            string? sube = null,
            string? satisTarihi = null,
            string? filter = null,
            int? year = null
            ) //SAYFALAMA KULLANILACAK
        
        {
            return await _satisService.getAllWithUrunAndSubeAsync(
            offset, limit, sort, order, urun, sube, satisTarihi, filter, year ?? DateTime.Now.Year
            );
        }

        [HttpGet("getSatisCountAsync")]
        public async Task<Result<IEnumerable<int>>> getSatisCountAsync(
             int offset = 1,
            int limit = 10,
            string sort = "id",
            string order = "asc",
            string? urun = null,
            string? sube = null,
            string? satisTarihi = null,
            string? filter = null,
            int? year = null
            )
        {
            return await _satisService.getSatisCountAsync(offset,limit, sort, order,urun,sube,satisTarihi,filter, year ?? DateTime.Now.Year);
        }




        /// <summary>
        /// Date time null olabilir, null ise tüm tarihler arasında getirir, tedarikciye ait olanları tarihler arasında getirir, tedarikciye ait olanları getirir,
        /// Tarih Formati (MM/DD/YYYY)
        /// </summary>
        /// <param name="tedarikciId"></param>
        /// <param name="ilkTarih">Tarih Formati (MM/DD/YYYY)</param>
        /// <param name="ikinciTarih">(MM/DD/YYYY)</param>
        /// <returns></returns>
        [HttpGet("GetAllWithUrunAndSubeByTedarikciId")] //Kullanıcı ulaşabilir
        public Result<ICollection<SatisDto>> GetAllWithUrunAndSubeByTedarikciId(string tedarikciId, DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            return _satisService.getAllWithUrunAndSubeByTedarikciId(tedarikciId, ilkTarih, ikinciTarih, subeId, kategoriId, firmaId);
        }

        [HttpGet("GetAllWithDetailsByFilters")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<SatisDto>> GetAllWithUrunAndSube(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            return _satisService.getAllWithUrunAndSube(ilkTarih, ikinciTarih, subeId, kategoriId, firmaId);
        }

        [HttpPut]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<Satis> UpdateSatis(Satis satis, string satisId)
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
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Satis> DeleteSatis(string id)
        {
            _satisService.deleteSatis(Int32.Parse(id));

            return new Result<Satis>
            {
                Message = "Satis başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet("getSatisKar")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<ICollection<KarDto>> getSatisKar(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId, string? urunId)
        {
            return _satisService.getSatisKar(ilkTarih, ikinciTarih, subeId, kategoriId, firmaId, urunId);
        }

        [HttpGet("getkarsilastirmaliSatisRapor")]
        public Result<KarsilastirmaliSatisRapor> getkarsilastirmaliSatisRapor(string tedarikciId, string? firmaId, string? kategoriId, string? subeId, string? urunId, string? donem, DateTime? tarih1, DateTime? tarih2)
        {
            return _satisService.getkarsilastirmaliSatisRapor(tedarikciId, firmaId, kategoriId, subeId, urunId, donem, tarih1, tarih2);
        }
        [HttpGet("getSatislarCount")]
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<long> getSatislarLength()
        {
            var satisCount = _satisService.getAll().Data.Count;
            return new Result<long>
            {
                Data = satisCount,
                Message = "Satislar başarıyla getirildi",
                StatusCode = StatusCodes.Status200OK
            };
        }






    }
}
