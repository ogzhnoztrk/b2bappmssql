using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public class UrunService : IUrunService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UrunService> _logger;
        public UrunService(IUnitOfWork unitOfWork, ILogger<UrunService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void addUrun(Urun Urun)
        {
            try
            {
                _logger.LogInformation("Ürün Eklendi");
                _unitOfWork.Urun.Add(Urun);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün eklenirken hata olustu");
                throw;
            }
        }

        public void deleteUrun(Guid objectId)
        {
            try
            {
                _unitOfWork.Urun.Remove(_unitOfWork.Urun.GetFirstOrDefault(x=>x.UrunId == objectId).Data);
                _logger.LogInformation("urun Silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "urun Silinirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<Urun>> getAll()
        {
            try
            {
                _logger.LogInformation("Urunler Listelendi");
                return _unitOfWork.Urun.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Urunler Listelenirken Hata Oluştu");
                throw;
            }
        }

        public Result<Urun> getUrunById(Guid objectId)
        {
            try
            {
                _logger.LogInformation("Urun Id ye gore getirildi");
                return _unitOfWork.Urun.GetFirstOrDefault(x => x.UrunId == objectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Urun Id ye gore getirilirken hata olustu");
                throw;
            }

        }

        public Result<ICollection<UrunDto>> getAllWithKategoriAdiAndTedarikci()
        {
            try
            {
                var urunler = _unitOfWork.Urun.GetAll().Data;
                var urunDtos = new List<UrunDto>();

                if (urunler != null)
                {
                    foreach (var urun in urunler)
                    {

                        var kategori = _unitOfWork.Kategori.GetFirstOrDefault(x => x.KategoriId == urun.KategoriId).Data;
                        var tedarikci = _unitOfWork.Tedarikci.GetFirstOrDefault(x => x.TedarikciId == urun.TedarikciId).Data;
                        var urunDto = new UrunDto
                        {
                            Kategori = kategori,
                            Fiyat = urun.Fiyat,
                            Id = urun.UrunId.ToString(),
                            UrunAdi = urun.UrunAdi,
                            Tedarikci = tedarikci,
                            SatisFiyati = urun.SatisFiyati

                        };
                        urunDtos.Add(urunDto);
                    }
                }
                var result = new Result<ICollection<UrunDto>>
                {
                    Data = urunDtos,
                    Message = "Ürün Kategori  Bilgileri İle Getirildi",
                    StatusCode = 200 
                };


                _logger.LogInformation("Urunler Kategori Bilgileri İle Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Urunler Kategori Bilgileri İle Getirilirken Hata Oluştu");
                throw;
            }
        }

        public void updateUrun(Urun Urun, string urunId)
        {
            try
            {

                _unitOfWork.Urun.Update(Urun);
                _logger.LogInformation("Ürün guncellendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün guncellenirken hata olustu");
                throw;
            }
        }

        public Result<UrunDto> getUrunWithKategoriAndTedarikci(Guid objectId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.UrunId == objectId).Data;
                var kategori = _unitOfWork.Kategori.GetFirstOrDefault(x=>x.KategoriId == urun.KategoriId).Data;
                var tedarikci = new Tedarikci();
                if (urun.TedarikciId != null)
                {
                    tedarikci = _unitOfWork.Tedarikci.GetFirstOrDefault(x => x.TedarikciId == urun.TedarikciId).Data;

                }
                else
                {
                    tedarikci = new() { TedarikciId = Guid.NewGuid(), TedarikciAdi = "AA", TedarikciTel = "00" };


                }




                var result = new Result<UrunDto>
                {
                    Data = new UrunDto { Id = urun.UrunId.ToString(), Fiyat = urun.Fiyat, UrunAdi = urun.UrunAdi, Kategori = kategori, Tedarikci = tedarikci, SatisFiyati = urun.SatisFiyati },
                    Message = "Ürün Kategori Bilgileri İle Getirildi",
                    StatusCode = 200 
                };


                _logger.LogInformation("Ürün Kategori Bilgileri İle Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün Kategori Bilgileri İle Getirilirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<UrunDto>> getUrunlerWithDetailsByTedarikciId(string tedarikciId)
        {
            try
            {
                var urunler = _unitOfWork.Urun.GetAll(x => x.UrunId.ToString() == tedarikciId).Data;
                var urunDtos = new List<UrunDto>();

                if (urunler != null)
                {
                    foreach (var urun in urunler)
                    {

                        var kategori = _unitOfWork.Kategori.GetFirstOrDefault(x => x.KategoriId == urun.KategoriId).Data;
                        var tedarikci = _unitOfWork.Tedarikci.GetFirstOrDefault(x => x.TedarikciId == urun.TedarikciId).Data;
                        var urunDto = new UrunDto
                        {
                            Kategori = kategori,
                            Fiyat = urun.Fiyat,
                            Id = urun.UrunId.ToString(),
                            UrunAdi = urun.UrunAdi,
                            Tedarikci = tedarikci,
                            SatisFiyati = urun.SatisFiyati

                        };
                        urunDtos.Add(urunDto);
                    }
                }
                var result = new Result<ICollection<UrunDto>>
                {
                    Data = urunDtos,
                    Message = "Ürün Kategori  Bilgileri İle Getirildi",
                    StatusCode = 200 
                };


                _logger.LogInformation("Urunler Kategori Bilgileri İle Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Urunler Kategori Bilgileri İle Getirilirken Hata Oluştu");
                throw;
            }
        }
    }
}
