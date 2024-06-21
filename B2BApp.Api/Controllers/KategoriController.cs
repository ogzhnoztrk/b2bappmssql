﻿using B2BApp.Business.Abstract;
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
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriService _kategoriService;

        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }


        [HttpPost]
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
        public Result<Kategori> GetCompany(string id)
        {

            var kategori = _kategoriService.getKategoriById(ObjectId.Parse(id));
            return kategori;
        }
        [HttpGet("all")]
        public Result<ICollection<Kategori>> GetCompany()
        {
            var kategorilar = _kategoriService.getAll();
            return kategorilar;
        }
        [HttpPut]
        public Result<Kategori> UpdateCompany(KategoriDto kategoriDto, string kategoriId)
        {

            Kategori kategori = new Kategori
            {
                KategoriAdi = kategoriDto.KategoriAdi,
                Id= ObjectId.Parse(kategoriId)
            };

            _kategoriService.updateKategori(kategori, kategoriId);
            return new Result<Kategori>
            {
                Data = kategori,
                Message = "Kategori başarıyla güncellendi",
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete]
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