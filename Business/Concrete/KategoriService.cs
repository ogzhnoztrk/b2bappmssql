using B2BApp.DataAccess.Abstract;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
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
        public KategoriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addKategori(Kategori kategori)
        {
            try
            {
                _unitOfWork.Kategori.InsertOne(kategori);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteKategori(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Kategori.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Kategori>> getAll()
        {
            try
            {
                return _unitOfWork.Kategori.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<Kategori> getKategoriById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Kategori.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void updateKategori(Kategori kategori, string kategoriId)
        {
            try
            {
                _unitOfWork.Kategori.ReplaceOne(kategori, kategoriId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
