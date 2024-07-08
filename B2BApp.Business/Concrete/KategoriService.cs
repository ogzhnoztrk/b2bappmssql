using Amazon.Runtime.Internal.Util;
using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
    public class KategoriService : IKategoriService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<KategoriService> _logger;
        public KategoriService(IUnitOfWork unitOfWork, ILogger<KategoriService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void addKategori(Kategori kategori)
        {
            try
            {
                _logger.LogInformation("Kategori eklendi");
                _unitOfWork.Kategori.InsertOne(kategori);
            }
            catch (Exception ex)
            {
                _logger.LogError("Kategori eklenirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public void deleteKategori(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("KategoriSilindi");
                _unitOfWork.Kategori.DeleteById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Kategori silinirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public Result<ICollection<Kategori>> getAll()
        {
            try
            {
                _logger.LogInformation("Kategoriler getirildi");
                return _unitOfWork.Kategori.GetAll();
            }
            catch (Exception ex )
            {
                _logger.LogError("Kategoriler getirilirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }

        public Result<Kategori> getKategoriById(ObjectId objectId)
        {
            try
            {
                _logger.LogInformation("Kategori getirildi");
                return _unitOfWork.Kategori.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Kategori getirilirken hata oluştu {ex}", ex.Message);
                throw;
            }
            
        }

        public void updateKategori(Kategori kategori, string kategoriId)
        {
            try
            {
                _logger.LogInformation("Kategori güncellendi");
                _unitOfWork.Kategori.ReplaceOne(kategori, kategoriId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Kategori güncellenirken hata oluştu {ex}", ex.Message);
                throw;
            }
        }
    }
}
