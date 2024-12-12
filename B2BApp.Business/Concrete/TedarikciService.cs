using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
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
                _unitOfWork.Tedarikci.Add(Tedarikci);
                _logger.LogInformation("Tedarikçi Eklendi");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Eklenirken Hata Oluştu");
                throw;
            }
        }

        public void deleteTedarikci(Guid objectId)
        {
            try
            {
                _unitOfWork.Tedarikci.Remove(_unitOfWork.Tedarikci.GetFirstOrDefault(x=>x.TedarikciId == objectId).Data);
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

        public Result<Tedarikci> getTedarikciById(Guid objectId)
        {
            try
            {
                _logger.LogInformation("Tedarikçi Getirildi");
                return _unitOfWork.Tedarikci.GetFirstOrDefault(x=>x.TedarikciId == objectId);
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
                _unitOfWork.Tedarikci.Update(Tedarikci; _logger.LogInformation("Tedarikçi Güncellendi");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarikçi Güncellenirken Hata Oluştu");
                throw;
            }
        }
    }
}
