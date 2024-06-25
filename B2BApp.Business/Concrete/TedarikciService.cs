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
    public class TedarikciService : ITedarikciService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TedarikciService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addTedarikci(Tedarikci Tedarikci)
        {
            try
            {
                _unitOfWork.Tedarikci.InsertOne(Tedarikci);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteTedarikci(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Tedarikci.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Tedarikci>> getAll()
        {
            try
            {
                return _unitOfWork.Tedarikci.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<Tedarikci> getTedarikciById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Tedarikci.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public void updateTedarikci(Tedarikci Tedarikci, string TedarikciId)
        {
            try
            {
                _unitOfWork.Tedarikci.ReplaceOne(Tedarikci, TedarikciId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
