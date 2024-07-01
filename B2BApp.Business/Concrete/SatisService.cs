using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
    public class SatisService : ISatisService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SatisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void addSatis(Satis satis)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

                var toplam = urun.Fiyat * satis.SatisMiktari;

                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = toplam,
                    UrunId = satis.UrunId,
                    Id = satis.Id
                     
                };

                _unitOfWork.Satis.InsertOne(satisSon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteSatis(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Satis.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Satis>> getAll()
        {
            try
            {
                return _unitOfWork.Satis.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<SatisDto>> getAllWithUrunAndSube()
        {
            var satislar = _unitOfWork.Satis.GetAll().Data;

            var satisDTOs = new List<SatisDto>();

            foreach (var satis in satislar)
            {
                var satisDto = new SatisDto
                {
                    Id = satis.Id,
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    Toplam= satis.Toplam,
                    Sube = _unitOfWork.Sube.GetById(satis.SubeId).Data,
                    Urun = _unitOfWork.Urun.GetById(satis.UrunId).Data,
                    
                };
                satisDTOs.Add(satisDto);
            }

            var result = new Result<ICollection<SatisDto>> {Data = satisDTOs, Message = "Satışlar getirildi", StatusCode=200,Time=DateTime.Now };

            return result;
        }

        public Result<ICollection<SatisDto>> getAllWithUrunAndSubeByTedarikciId(string tedarikciId)
        {
            var urunler = _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data;
            var satislar = _unitOfWork.Satis.GetAll().Data;
            var subeler = _unitOfWork.Sube.GetAll().Data;
            var satislarDto = (
                from urun in urunler
                join satis in satislar on urun.Id equals satis.UrunId
                join sube in subeler on satis.SubeId equals sube.Id
                select new SatisDto { Id=satis.Id, SatisMiktari=satis.SatisMiktari,
                  SatisTarihi=satis.SatisTarihi,
                  Sube=sube,
                  Toplam = satis.Toplam,
                  Urun = urun
                
                }).ToList();

            var result = new Result<ICollection<SatisDto>>
            {
                Data = satislarDto,
                Message = "ürün satışı detayları ile getirldi",
                StatusCode = 200,
                Time = DateTime.Now

            };
            return result;
        }

        public Result<Satis> getSatisById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Satis.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Result<SatisDto> getWithUrunAndSube(ObjectId objectId)
        {
            var satis = _unitOfWork.Satis.GetById(objectId.ToString()).Data;
            var sube = _unitOfWork.Sube.GetById(satis.SubeId).Data;
            var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

            var result = new Result<SatisDto> { 
                Data = new SatisDto { Id = satis.Id, Urun = urun, Sube =sube, SatisMiktari=satis.SatisMiktari, SatisTarihi = satis.SatisTarihi, Toplam=satis.Toplam },
                Message = "Satis bilgileri getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };


            return result;
        }

        public void updateSatis(Satis satis, string satisId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

                var toplam = urun.Fiyat * satis.SatisMiktari;

                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = toplam,
                    UrunId = satis.UrunId,
                    Id = satis.Id

                };


                _unitOfWork.Satis.ReplaceOne(satisSon, satis.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
