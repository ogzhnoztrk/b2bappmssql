﻿using B2BApp.Core.Models.Concrete;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IKategoriService
    {
        void addKategori(Kategori kategori);
        void updateKategori(Kategori kategori, string kategoriId);
        void deleteKategori(ObjectId objectId);
        Result<ICollection<Kategori>> getAll();
        Result<Kategori> getKategoriById(ObjectId objectId);
    }
}
