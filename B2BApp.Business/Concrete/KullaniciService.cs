using B2BApp.Business.Concrete;
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

        public Result<ICollection<KullaniciDto>> getAllWithTedarikci()
        {
            var kullanicilar = _unitOfWork.Kullanici.GetAll().Data;
            var tedarikciler = _unitOfWork.Tedarikci.GetAll().Data;
            var kullanicilarDto = (
                from kullanici in kullanicilar
                join tedarikci in tedarikciler on kullanici.TedarikciId equals tedarikci.Id
                select new KullaniciDto
                {
                    Id = kullanici.Id,
                    Tedarikci = tedarikci, KullaniciAdi = kullanici.KullaniciAdi, SifreHash = kullanici.SifreHash, SifreSalt= kullanici.SifreSalt
                }).ToList();




            return new Result<ICollection<KullaniciDto>>
            {
                Data = kullanicilarDto,
                Message = "Kullanıclıar detayları ile birlikte getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };


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
