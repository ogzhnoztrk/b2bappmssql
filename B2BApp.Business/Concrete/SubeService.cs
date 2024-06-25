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

        public Result<ICollection<Sube>> getAll(string? firmaId)
        {
            try
            {
                if (firmaId == null)
                {
                    return _unitOfWork.Sube.GetAll();
                }
                else
                {
                    return _unitOfWork.Sube.FilterBy(x => x.FirmaId == firmaId);
                }



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

        public Result<ICollection<SubeDto>> getSubelerWithFirma()
        {
            var subeler = _unitOfWork.Sube.GetAll().Data;
            var subelerDTOs = new List<SubeDto>();
            foreach (var sube in subeler) 
            {
                var firma = _unitOfWork.Firma.GetById(sube.FirmaId).Data;
                subelerDTOs.Add(new SubeDto { Id = sube.Id, SubeAdi = sube.SubeAdi, SubeTel = sube.SubeTel, Firma = firma });


            }

            var result = new Result<ICollection<SubeDto>>
            {
                Data = subelerDTOs,
                Message = "Şubeler Firmaları İle Birlikte Getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };
            return result;

        }

        public Result<SubeDto> getSubeWithFirma(ObjectId objectId)
        {
            var sube = _unitOfWork.Sube.GetById(objectId.ToString()).Data;
            var firma = _unitOfWork.Firma.GetById(sube.FirmaId).Data;
            var subelerDTO = new SubeDto
            {
                Id = sube.Id,
                SubeAdi = sube.SubeAdi,
                SubeTel = sube.SubeTel,
                Firma = firma
            };
           

            var result = new Result<SubeDto>
            {
                Data = subelerDTO,
                Message = "Şubeler Firmaları İle Birlikte Getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };
            return result;
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
