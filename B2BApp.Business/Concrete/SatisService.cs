using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
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

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;
                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
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
                    Toplam = satis.Toplam,
                    Sube = _unitOfWork.Sube.GetById(satis.SubeId).Data,
                    Urun = _unitOfWork.Urun.GetById(satis.UrunId).Data,

                };
                satisDTOs.Add(satisDto);
            }

            var result = new Result<ICollection<SatisDto>> { Data = satisDTOs, Message = "Satışlar getirildi", StatusCode = 200, Time = DateTime.Now };

            return result;
        }



        public Result<ICollection<SatisDto>> getAllWithUrunAndSubeByTedarikciId
            (
                string tedarikciId,
                DateTime? ilkTarih,
                DateTime? ikinciTarih,
                string? subeId,
                string? kategoriId,
                string? firmaId
            )
        {
            //kategoriId null ise tüm ürünleri getirir, değilse sadece o ürünleri getirir
            var urunler = kategoriId == null ? _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data
                : _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId && x.KategoriId == kategoriId).Data;

            var satislar = _unitOfWork.Satis.GetAll().Data;

            //subeId null ise tüm şubeleri getirir, değilse sadece o şubeyi getirir
            var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;

            //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
            subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId == firmaId).ToList();

            //ilk ve ikinci tarih filtresi isetnmediğinde kullanılır
            ilkTarih = ilkTarih ?? DateTime.MinValue;
            ikinciTarih = ikinciTarih ?? DateTime.MaxValue;

            var satislarDto = (
                from urun in urunler
                join satis in satislar on urun.Id equals satis.UrunId
                join sube in subeler on satis.SubeId equals sube.Id
                where satis.SatisTarihi >= ilkTarih && satis.SatisTarihi <= ikinciTarih
                select new SatisDto
                {
                    Id = satis.Id,
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    Sube = sube,
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

        public Result<ICollection<SatisDto>> getAllWithUrunAndSube(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            //kategoriId null ise tüm ürünleri getirir, değilse sadece o ürünleri getirir
            var urunler = kategoriId == null ? _unitOfWork.Urun.GetAll().Data
                : _unitOfWork.Urun.FilterBy(x => x.KategoriId == kategoriId).Data;

            var satislar = _unitOfWork.Satis.GetAll().Data;


            //subeId null ise tüm şubeleri getirir, değilse sadece o şubeyi getirir
            var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;

            //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
            subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId == firmaId).ToList();

            //ilk ve ikinci tarih filtresi isetnmediğinde kullanılır
            ilkTarih = ilkTarih ?? DateTime.MinValue;
            ikinciTarih = ikinciTarih ?? DateTime.MaxValue;

            var satislarDto = (
                from urun in urunler
                join satis in satislar on urun.Id equals satis.UrunId
                join sube in subeler on satis.SubeId equals sube.Id
                where satis.SatisTarihi >= ilkTarih && satis.SatisTarihi <= ikinciTarih
                select new SatisDto
                {
                    Id = satis.Id,
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    Sube = sube,
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

            var result = new Result<SatisDto>
            {
                Data = new SatisDto { Id = satis.Id, Urun = urun, Sube = sube, SatisMiktari = satis.SatisMiktari, SatisTarihi = satis.SatisTarihi, Toplam = satis.Toplam },
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

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;

                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
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

        public Result<ICollection<KarDto>> getSatisKar(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId, string? urunId)
        {

            var satislar = _unitOfWork.Satis.GetAll().Data;

            satislar = ilkTarih == null ? satislar : satislar.Where(x => x.SatisTarihi >= ilkTarih).ToList();
            satislar = ikinciTarih == null ? satislar : satislar.Where(x => x.SatisTarihi <= ikinciTarih).ToList();
            satislar = subeId == null ? satislar : satislar.Where(x => x.SubeId == subeId).ToList();
            satislar = firmaId == null ? satislar : satislar.Where(x => _unitOfWork.Sube.GetById(x.SubeId).Data.FirmaId == firmaId).ToList();
            satislar = kategoriId == null ? satislar : satislar.Where(x => _unitOfWork.Urun.GetById(x.UrunId).Data.KategoriId == kategoriId).ToList();
            satislar = urunId == null ? satislar : satislar.Where(x => x.UrunId == urunId).ToList();

            var groupedSatislar = satislar.GroupBy(x => x.UrunId)
                                    .Select(x => new KarDto
                                    {
                                        Urun = _unitOfWork.Urun.GetById(x.Key).Data,
                                        Firma = _unitOfWork.Firma.GetById(_unitOfWork.Sube.GetById(x.First().SubeId).Data.FirmaId).Data,
                                        Sube = _unitOfWork.Sube.GetById(x.First().SubeId).Data,
                                        ToplamSatisFiyat = x.Sum(y => y.Toplam),
                                        ToplamKar = x.Sum(y => y.Toplam) - x.Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari),
                                        ToplamFiyat = (double)x.Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari),
                                        ToplamSatisMiktari = x.Sum(y => y.SatisMiktari),

                                    }).ToList();

            var result = new Result<ICollection<KarDto>>
            {
                Data = groupedSatislar,
                Message = "Kârlar getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };



            return result;
        }
    }
}
