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
    public class SubeService : ISubeService
    {

        private readonly IUnitOfWork _unitOfWork;
        public SubeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addSube(Sube Sube)
        {
            try
            {
                _unitOfWork.Sube.InsertOne(Sube);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteSube(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Sube.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Sube>> getAll()
        {
            try
            {
                return _unitOfWork.Sube.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<Sube> getSubeById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Sube.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void updateSube(Sube Sube, string subeId)
        {
            try
            {
                _unitOfWork.Sube.ReplaceOne(Sube, Sube.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
