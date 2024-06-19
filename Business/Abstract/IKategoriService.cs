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
    public interface IKategoriService
    {
        void addKategori(Kategori kategori);
        void updateKategori(Kategori kategori);
        void deleteKategori(ObjectId objectId);
        Result<ICollection<Kategori>> getAll();
        Result<Kategori> getKategoriById(ObjectId objectId);
    }
}
