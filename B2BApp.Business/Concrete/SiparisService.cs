using B2BApp.Business.Abstract;
using B2BApp.Core.Models.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Concrete
{
    public class SiparisService : ISiparisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SiparisService> _logger;


        public SiparisService(IUnitOfWork unitOfWork, ILogger<SiparisService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void addSiparis(Siparis siparis)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;

                var toplam = urun.Fiyat * siparis.Adet;

                var siparisSon = new Siparis
                {
                    Adet = siparis.Adet,
                    UrunId = siparis.UrunId,
                    Toplam = toplam,
                    SiparisTarih = siparis.SiparisTarih,
                    SubeId = siparis.SubeId,
                    Id = siparis.Id,
                    TedarikciId = urun.TedarikciId,
                    IsActive = true
                };



                _logger.Equals("Sipariş eklendi");
                _unitOfWork.Siparis.InsertOne(siparisSon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş eklenirken bir hata oluştu");
                throw;
            }
        }

        public Result<string> changeAktiflik(string siparisId)
        {
            try
            {
                var siparis = _unitOfWork.Siparis.GetById(siparisId).Data;

                siparis.IsActive = !siparis.IsActive;

                _unitOfWork.Siparis.ReplaceOne(siparis, siparisId);


                _logger.LogInformation("Siparişin aktiflik durumu başarıyla değiştirildi");
                return new Result<string>
                {
                    Data = siparisId,
                    Message = "Siparişin aktiflik durumu başarıyla değiştirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Siparişin aktiflik durumu değiştirilirken bir hata oluştu");
                throw;
            }
        }

        public void deleteSiparis(ObjectId objectId)
        {
            try
            {

                _unitOfWork.Siparis.DeleteById(objectId.ToString());

                _logger.LogInformation("Sipariş başarıyla silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş silinirken bir hata oluştu");
                throw;
            }
        }

        public Result<ICollection<Siparis>> getAll()
        {
            try
            {
                _logger.LogInformation("Tüm siparişler başarıyla getirildi");
                return _unitOfWork.Siparis.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm siparişler getirilirken bir hata oluştu");
                throw;
            }
        }

        public Result<ICollection<SiparisDto>> getAllWithDetails()
        {
            try
            {
                var siparisler = _unitOfWork.Siparis.GetAll().Data;
                ICollection<SiparisDto> siparisDTOs = new List<SiparisDto>();

                foreach (var siparis in siparisler)
                {
                    var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
                    var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
                    var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;

                    var siparisDto = new SiparisDto
                    {
                        Id = siparis.Id.ToString(),
                        Adet = siparis.Adet,
                        Tarih = siparis.SiparisTarih,
                        Sube = sube,
                        Urun = urun,
                        Tedarikci = tedarikci,
                        Toplam = siparis.Toplam,
                        IsActive = siparis.IsActive,
                    };
                    siparisDTOs.Add(siparisDto);
                }

                var result = new Result<ICollection<SiparisDto>>
                {
                    Data = siparisDTOs,
                    Message = "Siparişler detayları ile başarıyla getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };
                _logger.LogInformation("Siparişler detayları ile başarıyla getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Siparişler dteyları ile getirilirken bir hata oluştu.");
                throw;
            }
        }

        public Result<ICollection<SiparisDto>> getAllWithDetailsByFilters(DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi)
        {
            try
            {
                var siparisler = _unitOfWork.Siparis.GetAll().Data;
                siparisler = aktifMi == null ? siparisler : siparisler.Where(x => x.IsActive == aktifMi).ToList();
                siparisler = subeId == null ? siparisler : siparisler.Where(x => x.SubeId == subeId).ToList();
                siparisler = urunId == null ? siparisler : siparisler.Where(x => x.UrunId == urunId).ToList();
                siparisler = tarih1 == null ? siparisler : siparisler.Where(x => x.SiparisTarih >= tarih1).ToList();
                siparisler = tarih2 == null ? siparisler : siparisler.Where(x => x.SiparisTarih <= tarih2).ToList();

                ICollection<SiparisDto> siparisDTOs = new List<SiparisDto>();
                foreach (var siparis in siparisler)
                {
                    var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
                    var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
                    var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;

                    var siparisDto = new SiparisDto
                    {
                        Id = siparis.Id.ToString(),
                        Adet = siparis.Adet,
                        Tarih = siparis.SiparisTarih,
                        Sube = sube,
                        Urun = urun,
                        Tedarikci = tedarikci,
                        Toplam = siparis.Toplam,
                        IsActive = siparis.IsActive,
                    };
                    siparisDTOs.Add(siparisDto);
                }

                _logger.LogInformation("Siparişler detayları ile başarıyla getirildi");
                return new Result<ICollection<SiparisDto>>
                {
                    Data = siparisDTOs,
                    Message = "Siparişler detayları ile başarıyla getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Siparişler detayları ile getirilirken bir hata oluştu.");
                throw;
            }
        }

        public Result<ICollection<SiparisDto>> getAllWithDetailsByFiltersAndTedarikciId(
            string tedarikciId, DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi)
        {
            try
            {
                var siparisler = _unitOfWork.Siparis.FilterBy(x => x.TedarikciId == tedarikciId).Data;
                siparisler = aktifMi == null ? siparisler : siparisler.Where(x => x.IsActive == aktifMi).ToList();
                siparisler = subeId == null ? siparisler : siparisler.Where(x => x.SubeId == subeId).ToList();
                siparisler = urunId == null ? siparisler : siparisler.Where(x => x.UrunId == urunId).ToList();
                siparisler = tarih1 == null ? siparisler : siparisler.Where(x => x.SiparisTarih >= tarih1).ToList();
                siparisler = tarih2 == null ? siparisler : siparisler.Where(x => x.SiparisTarih <= tarih2).ToList();

                ICollection<SiparisDto> siparisDTOs = new List<SiparisDto>();
                foreach (var siparis in siparisler)
                {
                    var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
                    var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
                    var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;

                    var siparisDto = new SiparisDto
                    {
                        Id = siparis.Id.ToString(),
                        Adet = siparis.Adet,
                        Tarih = siparis.SiparisTarih,
                        Sube = sube,
                        Urun = urun,
                        Tedarikci = tedarikci,
                        Toplam = siparis.Toplam,
                        IsActive = siparis.IsActive,
                    };
                    siparisDTOs.Add(siparisDto);
                }

                _logger.LogInformation("Siparişler detayları ile başarıyla getirildi");
                return new Result<ICollection<SiparisDto>>
                {
                    Data = siparisDTOs,
                    Message = "Siparişler detayları ile başarıyla getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Siparişler detayları ile getirilirken bir hata oluştu.");
                throw;
            }

        }

        public Result<SiparisDto> getAllWithDetailsById(string siparisId)
        {
            try
            {
                var siparis = _unitOfWork.Siparis.GetById(siparisId).Data;

                var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
                var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;

                var siparisDto = new SiparisDto
                {
                    Id = siparis.Id.ToString(),
                    Adet = siparis.Adet,
                    Tarih = siparis.SiparisTarih,
                    Sube = sube,
                    Urun = urun,
                    Tedarikci = tedarikci,
                    Toplam = siparis.Toplam,
                    IsActive = siparis.IsActive
                };

                var result = new Result<SiparisDto>
                {
                    Data = siparisDto,
                    Message = "Sipariş detayları ile başarıyla getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };
                _logger.LogInformation("Sipariş detayları ile başarıyla getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş detaları ile getirilirken hata oluştu");
                throw;
            }
        }

        public Result<Siparis> getSiparisById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Sipariş başarıyla getirildi");
                return _unitOfWork.Siparis.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş getirilirken bir hata oluştu");
                throw;
            }
        }

        public void updateSiparis(Siparis siparis, string siparisId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;

                var toplam = urun.Fiyat * siparis.Adet;

                var siparisSon = new Siparis
                {
                    Adet = siparis.Adet,
                    UrunId = siparis.UrunId,
                    Toplam = toplam,
                    SiparisTarih = siparis.SiparisTarih,
                    SubeId = siparis.SubeId,
                    Id = siparis.Id,
                    TedarikciId = urun.TedarikciId,
                    IsActive = siparis.IsActive,
                };



                _logger.LogInformation("Sipariş başarıyla güncellendi");
                _unitOfWork.Siparis.ReplaceOne(siparisSon, siparisId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sipariş güncellenirken bir hata oluştu");
                throw;
            }
        }
    }
}
