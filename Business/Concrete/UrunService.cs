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
    public class UrunService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UrunService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addUrun(Urun Urun)
        {
            try
            {
                _unitOfWork.Urun.InsertOne(Urun);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteUrun(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Urun.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Urun>> getAll()
        {
            try
            {
                return _unitOfWork.Urun.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<Urun> getUrunById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Urun.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void updateUrun(Urun Urun)
        {
            try
            {
                _unitOfWork.Urun.ReplaceOne(Urun, Urun.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
