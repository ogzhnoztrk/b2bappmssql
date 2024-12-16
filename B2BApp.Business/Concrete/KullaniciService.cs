using B2BApp.Business.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public class KullaniciService : IKullaniciService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<KullaniciService> _logger;

        public KullaniciService(IUnitOfWork unitOfWork, ILogger<KullaniciService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        public void addKullanici(Kullanici kullanici)
        {
            try
            {
                _logger.LogInformation("Kullanıcı eklendi");
                _unitOfWork.Kullanici.Add(kullanici);
            }
            catch (Exception ex)
            {
                _logger.LogError("Kullanıcı eklenirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public void deleteKullanici(Guid objectId)
        {
            try
            {
                _logger.LogInformation("Kullanıcı silindi");
                _unitOfWork.Kullanici.Remove(_unitOfWork.Kullanici.GetFirstOrDefault(x=>x.Id == objectId).Data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Kullanıcı silinirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public Result<ICollection<Kullanici>> getAll()
        {
            try
            {
                _logger.LogInformation("Kullanıcılar getirildi");
                return _unitOfWork.Kullanici.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Kullanıcılar getirilirken hata oluştu {ex}", ex.Message);
                throw;
            }

        }

        public Result<ICollection<KullaniciDto>> getAllWithTedarikci()
        {
            var kullanicilar = _unitOfWork.Kullanici.GetAll().Data;
            var tedarikciler = _unitOfWork.Tedarikci.GetAll().Data;
            var kullanicilarDto = (
                from kullanici in kullanicilar
                join tedarikci in tedarikciler on kullanici.TedarikciId equals tedarikci.Id
                select new KullaniciDto
                {
                    Id = kullanici.Id.ToString(),
                    Tedarikci = tedarikci,
                    KullaniciAdi = kullanici.KullaniciAdi,
                    SifreHash = kullanici.SifreHash,
                    SifreSalt = kullanici.SifreSalt
                }).ToList();



            _logger.LogInformation("Kullanıclıar detayları ile birlikte getirildi");
            return new Result<ICollection<KullaniciDto>>
            {
                Data = kullanicilarDto,
                Message = "Kullanıclıar detayları ile birlikte getirildi",
                StatusCode = 200
            };


        }

        public Result<Kullanici> getKullaniciById(Guid objectId)
        {
            try
            {
                _logger.LogInformation("Kullanıcı getirildi");
                return _unitOfWork.Kullanici.GetFirstOrDefault(x=>x.Id == objectId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Kullanıcı getirilirken  hata {ex}", ex.Message);

                throw;
            }
        }

        public void updateKullanici(Kullanici kullanici, string kullaniciId)
        {
            try
            {
                _unitOfWork.Kullanici.Update(kullanici);
                _logger.LogInformation("Kullanıcı güncellendi");

            }
            catch (Exception ex)
            {
                _logger.LogError("Kullanıcı güncellenirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }
    }
}
