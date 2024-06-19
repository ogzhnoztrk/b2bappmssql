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
    public class SatisService :ISatisService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SatisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void addSatis(Satis Satis)
        {
            try
            {
                _unitOfWork.Satis.InsertOne(Satis);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteSatis(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Satis.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Satis>> getAll()
        {
            try
            {
                return _unitOfWork.Satis.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<Satis> getSatisById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Satis.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void updateSatis(Satis Satis)
        {
            try
            {
                _unitOfWork.Satis.ReplaceOne(Satis, Satis.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
