using B2BApp.Business.Abstract;
using B2BApp.Core.Models.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Concrete
{
    public class SubeService : ISubeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubeService> _logger;
        public SubeService(IUnitOfWork unitOfWork, ILogger<SubeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void addSube(Sube Sube)
        {
            try
            {
                _unitOfWork.Sube.InsertOne(Sube);
                _logger.LogInformation("Şube Eklendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Eklenirken Hata Oluştu");
                throw;
            }
        }

        public void deleteSube(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Şube Silindi");
                _unitOfWork.Sube.DeleteById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Silinirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<Sube>> getAll(string? firmaId)
        {
            try
            {
                if (firmaId == null)
                {
                    _logger.LogInformation("Tüm Şubeler Getirildi");
                    return _unitOfWork.Sube.GetAll();
                }
                else
                {
                    _logger.LogInformation("Firmaya göre Tüm Şubeler Getirildi");

                    return _unitOfWork.Sube.FilterBy(x => x.FirmaId == firmaId);
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şubeler Getirilirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<Sube>> getSubeByFirmaId(string firmaId)
        {
            try
            {
                _logger.LogInformation("Firmaya Göre Şubeler Getirildi");
                return _unitOfWork.Sube.FilterBy(x => x.FirmaId == firmaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Firmaya Göre Şubeler Getirilirken Hata Oluştu");
                throw;
            }
        }

        public Result<Sube> getSubeById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Şube Getirildi");
                return _unitOfWork.Sube.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Getirilirken Hata Oluştu");
                throw;
            }

        }

        public Result<ICollection<SubeDto>> getSubelerWithFirma()
        {
            try
            {
                var subeler = _unitOfWork.Sube.GetAll().Data;
                var subelerDTOs = new List<SubeDto>();
                foreach (var sube in subeler)
                {
                    var firma = _unitOfWork.Firma.GetById(sube.FirmaId).Data;
                    subelerDTOs.Add(new SubeDto { Id = sube.Id.ToString(), SubeAdi = sube.SubeAdi, SubeTel = sube.SubeTel, Firma = firma });


                }

                var result = new Result<ICollection<SubeDto>>
                {
                    Data = subelerDTOs,
                    Message = "Şubeler Firmaları İle Birlikte Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };
                _logger.LogInformation("Şubeler Firmaları İle Birlikte Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şubeler Firmaları İle Birlikte Getirilirken Hata Oluştu");
                throw;
            }

        }

        public Result<SubeDto> getSubeWithFirma(ObjectId objectId)
        {
            try
            {
                var sube = _unitOfWork.Sube.GetById(objectId.ToString()).Data;
                var firma = _unitOfWork.Firma.GetById(sube.FirmaId).Data;
                var subelerDTO = new SubeDto
                {
                    Id = sube.Id.ToString(),
                    SubeAdi = sube.SubeAdi,
                    SubeTel = sube.SubeTel,
                    Firma = firma
                };


                var result = new Result<SubeDto>
                {
                    Data = subelerDTO,
                    Message = "Şubeler Firmaları İle Birlikte Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };
                _logger.LogInformation("Şubeler Firmaları İle Birlikte Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şubeler Firmaları İle Birlikte Getirilirken Hata Oluştu");
                throw;
            }
        }

        public void updateSube(Sube Sube, string subeId)
        {
            try
            {
                _logger.LogInformation("Şube Güncellendi");
                _unitOfWork.Sube.ReplaceOne(Sube, Sube.Id.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şube Güncellenirken Hata Oluştu");
                throw;
            }
        }


    }
}
