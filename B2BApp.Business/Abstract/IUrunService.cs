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
    public interface IUrunService
    {

        void addUrun(Urun urunler);
        void updateUrun(Urun urunler, string urunId);
        void deleteUrun(ObjectId objectId);
        Result<ICollection<Urun>> getAll();
        Result<ICollection<UrunDto>> getAllWithKategoriAdi();
        Result<UrunDto> getUrunWithKategori(ObjectId objectId);

        Result<Urun> getUrunById(ObjectId objectId);

    }
}
