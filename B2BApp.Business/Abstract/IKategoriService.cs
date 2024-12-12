using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IKategoriService
    {
        void addKategori(Kategori kategori);
        void updateKategori(Kategori kategori, string kategoriId);
        void deleteKategori(Guid objectId);
        Result<ICollection<Kategori>> getAll();
        Result<Kategori> getKategoriById(Guid objectId);
    }
}
