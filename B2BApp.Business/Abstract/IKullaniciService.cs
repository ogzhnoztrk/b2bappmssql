using B2BApp.Core.Models.Concrete;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IKullaniciService
    {

        void addKullanici(Kullanici kullanici);
        void updateKullanici(Kullanici kullanici, string kullaniciId);
        void deleteKullanici(ObjectId objectId);
        Result<ICollection<Kullanici>> getAll();
        Result<ICollection<KullaniciDto>> getAllWithTedarikci();
        Result<Kullanici> getKullaniciById(ObjectId objectId);

    }
}
