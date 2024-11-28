using B2BApp.Business.Abstract;
using B2BApp.Core.Models.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Concrete
{
    public class TedarikciService : ITedarikciService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TedarikciService> _logger;

        public TedarikciService(IUnitOfWork unitOfWork, ILogger<TedarikciService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void addTedarikci(Tedarikci Tedarikci)
        {
            try
            {
                _unitOfWork.Tedarikci.InsertOne(Tedarikci);
                _logger.LogInformation("Tedarikçi Eklendi");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Eklenirken Hata Oluştu");
                throw;
            }
        }

        public void deleteTedarikci(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Tedarikci.DeleteById(objectId.ToString());
                _logger.LogInformation("Tedarikçi Silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Silinirken Hata Oluştu");
                throw;
            }
        }

        public Result<ICollection<Tedarikci>> getAll()
        {
            try
            {
                _logger.LogInformation("Tedarikçiler Listelendi");
                return _unitOfWork.Tedarikci.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçiler Listelenirken Hata Oluştu");
                throw;
            }
        }

        public Result<Tedarikci> getTedarikciById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Tedarikçi Getirildi");
                return _unitOfWork.Tedarikci.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Getirilirken Hata Oluştu");
                throw;
            }
        }

        public void updateTedarikci(Tedarikci Tedarikci, string TedarikciId)
        {
            try
            {
                _unitOfWork.Tedarikci.ReplaceOne(Tedarikci, TedarikciId); _logger.LogInformation("Tedarikçi Güncellendi");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Güncellenirken Hata Oluştu");
                throw;
            }
        }
    }
}
