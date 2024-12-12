using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IUrunService
    {

        void addUrun(Urun urunler);
        void updateUrun(Urun urunler, string urunId);
        void deleteUrun(Guid objectId);
        Result<ICollection<Urun>> getAll();
        Result<ICollection<UrunDto>> getAllWithKategoriAdiAndTedarikci();
        Result<UrunDto> getUrunWithKategoriAndTedarikci(Guid objectId);
        Result<ICollection<UrunDto>> getUrunlerWithDetailsByTedarikciId(string tedarikciId);

        Result<Urun> getUrunById(Guid objectId);

    }
}
