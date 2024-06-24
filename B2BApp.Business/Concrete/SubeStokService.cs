using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
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

        public Result<ICollection<SubeStokDto>> getAllWithSubeAndUrun()
        {
            var subeStoklar = _unitOfWork.SubeStok.GetAll().Data;
            var subeStokDTOs = new List<SubeStokDto>();
            foreach (var subeStok in subeStoklar)
            {
                var urun = _unitOfWork.Urun.GetById(subeStok.UrunId).Data;
                var sube = _unitOfWork.Sube.GetById(subeStok.SubeId).Data;
                subeStokDTOs.Add(new SubeStokDto
                {
                    id = subeStok.Id,
                    Stok = subeStok.Stok,
                    Sube = sube,
                    Urun = urun
                });

            }
            var result = new Result<ICollection<SubeStokDto>>
            {
                Data = subeStokDTOs,
                Message = "Şubelerin Stokları Detayları İle Getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };
            return result;
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

        public Result<SubeStokDto> getWithSubeAndUrun(ObjectId objectId)
        {
            var subeStok = _unitOfWork.SubeStok.GetById(objectId.ToString()).Data;

            var sube = _unitOfWork.Sube.GetById(subeStok.SubeId).Data;
            var urun = _unitOfWork.Urun.GetById(subeStok.UrunId).Data;

            var subeStokDto = new SubeStokDto
            {
                id = subeStok.Id,
                Stok = subeStok.Stok,
                Sube = sube,
                Urun = urun,
            };

            var result = new Result<SubeStokDto>
            {
                Data = subeStokDto,
                Message = "Seçili Ürünün Şube Stoğu Getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };

            return result;
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
