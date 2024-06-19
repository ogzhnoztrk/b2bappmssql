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
    public class FirmaService : IFirmaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FirmaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addFirma(Firma firma)
        {
            try
            {
                _unitOfWork.Firma.InsertOne(firma);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteFirma(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Firma.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Firma>> getAll()
        {
            try
            {
                return _unitOfWork.Firma.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<Firma> getFirmaById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Firma.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public void updateFirma(Firma firma)
        {
            try
            {
                _unitOfWork.Firma.ReplaceOne(firma, firma.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
