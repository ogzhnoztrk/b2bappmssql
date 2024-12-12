using B2BApp.Business.Concrete;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IKullaniciService
    {

        void addKullanici(Kullanici kullanici);
        void updateKullanici(Kullanici kullanici, string kullaniciId);
        void deleteKullanici(   Guid objectId);
        Result<ICollection<Kullanici>> getAll();
        Result<ICollection<KullaniciDto>> getAllWithTedarikci();
        Result<Kullanici> getKullaniciById(Guid objectId);

    }
}
