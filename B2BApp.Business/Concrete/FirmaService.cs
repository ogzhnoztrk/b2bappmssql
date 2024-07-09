using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public class FirmaService : IFirmaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FirmaService> _logger;
        public FirmaService(IUnitOfWork unitOfWork, ILogger<FirmaService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void addFirma(Firma firma)
        {
            try
            {
                _logger.LogInformation("Firma eklendi");
                _unitOfWork.Firma.InsertOne(firma);
            }
            catch (Exception ex)
            {
                _logger.LogError("Firma eklenirken hata oluştu {ex}", ex.Message);

                throw;
            }
        }

        public void deleteFirma(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Firma Silindi");
                _unitOfWork.Firma.DeleteById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Firma silinirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public Result<ICollection<Firma>> getAll()
        {
            try
            {
                _logger.LogInformation("Firmalar getirildi");
                return _unitOfWork.Firma.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError("Firmalar getirilirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public Result<Firma> getFirmaById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Firma getirildi");
                return _unitOfWork.Firma.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Firma getirilirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public void updateFirma(Firma firma, string firmaId)
        {
            try
            {
                _logger.LogWarning("Firma Güncellendi");
                _unitOfWork.Firma.ReplaceOne(firma, firmaId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Firma güncellenirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }
    }
}
