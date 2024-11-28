using B2BApp.Core.Models.Concrete;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IUrunService
    {

        void addUrun(Urun urunler);
        void updateUrun(Urun urunler, string urunId);
        void deleteUrun(ObjectId objectId);
        Result<ICollection<Urun>> getAll();
        Result<ICollection<UrunDto>> getAllWithKategoriAdiAndTedarikci();
        Result<UrunDto> getUrunWithKategoriAndTedarikci(ObjectId objectId);
        Result<ICollection<UrunDto>> getUrunlerWithDetailsByTedarikciId(string tedarikciId);

        Result<Urun> getUrunById(ObjectId objectId);

    }
}
