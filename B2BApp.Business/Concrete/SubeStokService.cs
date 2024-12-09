﻿using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace B2BApp.Business.Abstract
{
    public class SubeStokService : ISubeStokService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubeStokService> _logger;
        public SubeStokService(IUnitOfWork unitOfWork, ILogger<SubeStokService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
     
        public void addManySubeStok(List<SubeStok> subeStokList)
        {
            //şubeId ve urunid aynı olan verileri sil
            var duplicateStoklar = new List<SubeStok>();
            
            foreach (var subeStok in subeStokList)
            {
                var duplicateStok = _unitOfWork.SubeStok.FilterBy(x => x.SubeId == subeStok.SubeId && x.UrunId == subeStok.UrunId).Data;
                duplicateStoklar.AddRange(duplicateStok);
            }

            foreach (var duplicateStok in duplicateStoklar)
            {
                _unitOfWork.SubeStok.DeleteById(duplicateStok.Id.ToString());
            }

            //yeni verileri ekle
            _unitOfWork.SubeStok.InsertMany(subeStokList);
        }

        public void addSubeStok(SubeStok SubeStok)
        {
            try
            {
                if (_unitOfWork.SubeStok.FilterBy(x => x.SubeId == SubeStok.SubeId && x.UrunId == SubeStok.UrunId).Data.Count > 0)
                {
                    _logger.LogError("Şube Stok Eklenirken Hata Oluştu: Aynı Ürün Zaten Ekli");
                    throw new Exception("Aynı Ürün Zaten Ekli");
                }
                _logger.LogInformation("Şube Stok Eklendi");
                _unitOfWork.SubeStok.InsertOne(SubeStok);
            }
            catch (Exception ex)
            {
                _logger.LogError("Şube Stok Eklenirken Hata Oluştu");
                throw;
            }
        }

        public void deleteSubeStok(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Şube Stok Silindi");
                _unitOfWork.SubeStok.DeleteById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Stok Silinirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<SubeStok>> getAll()
        {
            try
            {
                _logger.LogInformation("Tüm Şube Stokları Getirildi");
                return _unitOfWork.SubeStok.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm Şube Stokları Getirilirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<SubeStokDto>> getAllWithDetailsByFilters(string? subeId, string? firmaId, string? kategoriId)
        {
            try
            {
                //kategoriId null ise tüm ürünlerin stoklarını getir, değilse sadece seçili kategorinin stoklarını getir
                var urunler = kategoriId == null ? _unitOfWork.Urun.GetAll().Data : _unitOfWork.Urun.FilterBy(x => x.KategoriId == kategoriId).Data;
                //subeId null ise tüm şubelerin stoklarını getir, değilse sadece seçili şubenin stoklarını getir
                var sube = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;
                //firmaId null ise tüm firmaların stoklarını getir, değilse sadece seçili firmanın stoklarını getir
                sube = firmaId == null ? sube : sube.Where(x => x.FirmaId == firmaId).ToList();

                var subeStoklar = _unitOfWork.SubeStok.GetAll().Data;

                var subeStokDTOs = (from urun in urunler
                                    join subeStok in subeStoklar on urun.Id equals subeStok.UrunId

                                    join sub in sube on subeStok.SubeId equals sub.Id
                                    select new SubeStokDto
                                    {
                                        id = subeStok.Id,
                                        Stok = subeStok.Stok,
                                        Sube = sub,
                                        Urun = urun,

                                    }).ToList();

                var result = new Result<ICollection<SubeStokDto>>
                {
                    Data = subeStokDTOs,
                    Message = "Şubelerin Stokları Detayları İle Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };

                _logger.LogInformation("Şubelerin Stokları Detayları İle Getirildi");

                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şubelerin Stokları Detayları İle Getirilirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<SubeStokDto>> getAllWithSubeAndUrun()
        {
            try
            {
                var subeStoklar = _unitOfWork.SubeStok.GetAll().Data;
                var subeStokDTOs = new List<SubeStokDto>();
                foreach (var subeStok in subeStoklar)
                {
                    var urun = _unitOfWork.Urun.GetById(subeStok.UrunId).Data;
                    var sube = _unitOfWork.Sube.GetById(subeStok.SubeId).Data;
                    subeStokDTOs.Add(new SubeStokDto
                    {
                        id = subeStok.Id,
                        Stok = subeStok.Stok,
                        Sube = sube,
                        Urun = urun
                    });

                }
                var result = new Result<ICollection<SubeStokDto>>
                {
                    Data = subeStokDTOs,
                    Message = "Şubelerin Stokları Detayları İle Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };
                _logger.LogInformation("Şubelerin Stokları Detayları İle Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Şubelerin Stokları Detayları İle Getirilirke Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<SubeStokDto>> getAllWithSubeAndUrunByTedarikciId(string tedarikciId, string? subeId, string? firmaId, string? kategoriId)
        {
            try
            {
                //tedarikciId null ise tüm ürünlerin stoklarını getir, değilse sadece seçili tedarikcinin stoklarını getir
                var urunler = _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data;

                //kategoriId null ise tüm kategorilerin stoklarını getir, değilse sadece seçili kategorinin stoklarını getir
                urunler = kategoriId == null ? urunler : urunler.Where(x => x.KategoriId == kategoriId).ToList();


                //subeId null ise tüm şubelerin stoklarını getir, değilse sadece seçili şubenin stoklarını getir
                var sube = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;
                //firmaId null ise tüm firmaların stoklarını getir, değilse sadece seçili firmanın stoklarını getir
                sube = firmaId == null ? sube : sube.Where(x => x.FirmaId == firmaId).ToList();

                var subeStoklar = _unitOfWork.SubeStok.GetAll().Data;

                var subeStokDTOs = (from urun in urunler
                                    join subeStok in subeStoklar on urun.Id equals subeStok.UrunId
                                    where urun.TedarikciId == tedarikciId

                                    join sub in sube on subeStok.SubeId equals sub.Id
                                    select new SubeStokDto
                                    {
                                        id = tedarikciId,
                                        Stok = subeStok.Stok,
                                        Sube = sub,
                                        Urun = urun,

                                    }).ToList();

                var result = new Result<ICollection<SubeStokDto>>
                {
                    Data = subeStokDTOs,
                    Message = "Şubelerin Stokları Detayları İle Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };
                _logger.LogInformation("Şubelerin Stokları Detayları İle Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şubelerin Stokları Detayları İle Getirilirken Hata Oluştu");
                throw;
            }

        }

        public Result<SubeStok> getSubeStokById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Şube Stok Getirildi");
                return _unitOfWork.SubeStok.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Stok Getirilirken Hata Oluştu");
                throw;
            }

        }

        public Result<SubeStokDto> getWithSubeAndUrun(ObjectId objectId)
        {
            try
            {
                var subeStok = _unitOfWork.SubeStok.GetById(objectId.ToString()).Data;

                var sube = _unitOfWork.Sube.GetById(subeStok.SubeId).Data;
                var urun = _unitOfWork.Urun.GetById(subeStok.UrunId).Data;

                var subeStokDto = new SubeStokDto
                {
                    id = subeStok.Id,
                    Stok = subeStok.Stok,
                    Sube = sube,
                    Urun = urun,
                };

                var result = new Result<SubeStokDto>
                {
                    Data = subeStokDto,
                    Message = "Seçili Ürünün Şube Stoğu Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };

                _logger.LogInformation("Seçili Ürünün Şube Stoğu Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Seçili Ürünün Şube Stoğu Getirilirken Hata Oluştu");
                throw;
            }
        }

        public void updateSubeStok(SubeStok SubeStok, string subeStokId)
        {
            try
            {
                _logger.LogInformation("Şube Stok Güncellendi");
                _unitOfWork.SubeStok.ReplaceOne(SubeStok, SubeStok.Id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Stok Güncellenirken Hata Oluştu");
                throw;
            }
        }


        #region

        #endregion
    }
}
