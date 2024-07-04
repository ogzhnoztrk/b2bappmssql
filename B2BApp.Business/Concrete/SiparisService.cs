﻿using B2BApp.DataAccess.Abstract;
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
    public class SiparisService : ISiparisService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiparisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void addSiparis(Siparis siparis)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;

                var toplam = urun.Fiyat * siparis.Adet;

                var siparisSon = new Siparis
                {
                    Adet = siparis.Adet,
                    UrunId = siparis.UrunId,
                    Toplam = toplam,
                    SiparisTarih = siparis.SiparisTarih,
                    SubeId = siparis.SubeId,
                    Id = siparis.Id,
                    TedarikciId = urun.TedarikciId,
                    IsActive = true
                };




                _unitOfWork.Siparis.InsertOne(siparisSon);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void deleteSiparis(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Siparis.DeleteById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<ICollection<Siparis>> getAll()
        {
            try
            {
                return _unitOfWork.Siparis.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<ICollection<SiparisDto>> getAllWithDetails()
        {
            var siparisler = _unitOfWork.Siparis.GetAll().Data;
            ICollection<SiparisDto> siparisDTOs = new List<SiparisDto>();
           
            foreach (var siparis in siparisler)
            {
                var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
                var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;
                
                var siparisDto = new SiparisDto
                {
                    Id = siparis.Id,
                    Adet = siparis.Adet,                  
                    Tarih = siparis.SiparisTarih,
                    Sube = sube,
                    Urun = urun,
                    Tedarikci = tedarikci,
                    Toplam = siparis.Toplam,
                    IsActive = siparis.IsActive,
                };
                siparisDTOs.Add(siparisDto);
            }

            var result = new Result<ICollection<SiparisDto>>
            {
                Data = siparisDTOs,
                Message = "Siparişler detayları ile başarıyla getirildi",
                StatusCode = 200,
                Time=DateTime.Now
            };

            return result;
        }

        public Result<SiparisDto> getAllWithDetailsById(string siparisId)
        {
            var siparis = _unitOfWork.Siparis.GetById(siparisId).Data;

            var sube = _unitOfWork.Sube.GetById(siparis.SubeId).Data;
            var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;
            var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;

            var siparisDto = new SiparisDto
            {
                Id = siparis.Id,
                Adet = siparis.Adet,
                Tarih = siparis.SiparisTarih,
                Sube = sube,
                Urun = urun,
                Tedarikci = tedarikci,
                Toplam = siparis.Toplam,
                IsActive = siparis.IsActive
            };

            var result = new Result<SiparisDto>
            {
                Data = siparisDto,
                Message = "Sipariş detayları ile başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };

            return result;
        }

        public Result<Siparis> getSiparisById(ObjectId objectId)
        {
            try
            {
                return _unitOfWork.Siparis.GetById(objectId.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public void updateSiparis(Siparis siparis, string siparisId)
        {
            try
            { 
                var urun = _unitOfWork.Urun.GetById(siparis.UrunId).Data;

                var toplam = urun.Fiyat * siparis.Adet;

                var siparisSon = new Siparis
                {
                    Adet = siparis.Adet,
                    UrunId = siparis.UrunId,
                    Toplam = toplam,
                    SiparisTarih = siparis.SiparisTarih,
                    SubeId = siparis.SubeId,
                    Id = siparis.Id,
                    TedarikciId = urun.TedarikciId,
                    IsActive = siparis.IsActive,
                };




                _unitOfWork.Siparis.ReplaceOne(siparisSon,siparisId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
