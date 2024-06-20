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
    public class SubeStokService : ISubeStokService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubeStokService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void addSubeStok(SubeStok SubeStok)
        {
            try
            {
                _unitOfWork.SubeStok.InsertOne(SubeStok);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteSubeStok(ObjectId objectId)
        {
            try
            {
                _unitOfWork.SubeStok.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<SubeStok>> getAll()
        {
            try
            {
                return _unitOfWork.SubeStok.GetAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<SubeStok> getSubeStokById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.SubeStok.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void updateSubeStok(SubeStok SubeStok, string subeStokId)
        {
            try
            {
                _unitOfWork.SubeStok.ReplaceOne(SubeStok, SubeStok.Id.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
