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
    public class KullaniciService : IKullaniciService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KullaniciService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addKullanici(Kullanici kullanici)
        {
            try
            {
                _unitOfWork.Kullanici.InsertOne(kullanici);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteKullanici(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Kullanici.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Kullanici>> getAll()
        {
            try
            {
                return _unitOfWork.Kullanici.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<Kullanici> getKullaniciById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Kullanici.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public void updateKullanici(Kullanici kullanici, string kullaniciId)
        {
            try
            {
                _unitOfWork.Kullanici.ReplaceOne(kullanici, kullaniciId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
