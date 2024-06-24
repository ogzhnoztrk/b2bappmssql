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
    public class UrunService : IUrunService
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

        public Result<ICollection<UrunDto>> getAllWithKategoriAdi()
        {
            try
            {
                var urunler = _unitOfWork.Urun.GetAll().Data;
                 var urunDtos = new List<UrunDto>();

                if (urunler != null)
                {
                    foreach (var urun in urunler)
                    {

                        var kategori = _unitOfWork.Kategori.GetById(urun.KategoriId).Data;
                        var urunDto = new UrunDto
                        {
                        Kategori = kategori,
                        Fiyat = urun.Fiyat,
                        Id = urun.Id,
                        UrunAdi = urun.UrunAdi
                        };
                        urunDtos.Add(urunDto);
                    }
                }


            

                var result = new Result<ICollection<UrunDto>>
                {
                    Data = urunDtos,
                    Message = "Ürün Kategori  Bilgileri İle Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };



                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void updateUrun(Urun Urun, string urunId)
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

        public Result<UrunDto> getUrunWithKategori(ObjectId objectId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(objectId.ToString()).Data;
                var kategori = _unitOfWork.Kategori.GetById(urun.KategoriId.ToString()).Data;




                var result = new Result<UrunDto>
                {
                    Data = new UrunDto { Id = urun.Id, Fiyat= urun.Fiyat, UrunAdi= urun.UrunAdi, Kategori = kategori},
                    Message = "Ürün Kategori Bilgileri İle Getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now,
                };



                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
