﻿using B2BApp.Business.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class KategoriController : ControllerBase
    {
        private readonly IKategoriService _kategoriService;

        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }


        [HttpPost]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<Kategori> PostCompany(Kategori kategori)
        {

            _kategoriService.addKategori(kategori);
            return new Result<Kategori>
            {
                Data = kategori,
                Message = "Kategori başarıyla eklendi",
                StatusCode = StatusCodes.Status200OK
            };
        }
        [HttpGet]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Kategori> GetCompany(string id)
        {

            var kategori = _kategoriService.getKategoriById(ObjectId.Parse(id));
            return kategori;
        }

        [HttpGet("all")]
        // [Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<ICollection<Kategori>> GetCompany()
        {
            var kategoriler = _kategoriService.getAll();

            return kategoriler;
        }
        [HttpPut]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Kategori> UpdateCompany(Kategori kategori, string kategoriId)
        {

            //Kategori kategori = new Kategori
            //{
            //    KategoriAdi = kategoriDto.KategoriAdi,
            //    Id= ObjectId.Parse(kategoriId)
            //};

            _kategoriService.updateKategori(kategori, kategoriId);
            return new Result<Kategori>
            {
                Data = kategori,
                Message = "Kategori başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
        [Authorize(Roles = "6682972f420b0208d3d620a7")]
        public Result<Kategori> DeleteCompany(string id)
        {
            _kategoriService.deleteKategori(ObjectId.Parse(id));

            return new Result<Kategori>
            {
                Message = "Kategori başarıyla silindi",
                StatusCode = StatusCodes.Status200OK
            };
        }


    }
}
