﻿using B2BApp.Core.Models.Concrete;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface ITedarikciService
    {

        void addTedarikci(Tedarikci Tedarikciler);
        void updateTedarikci(Tedarikci Tedarikciler, string TedarikciId);
        void deleteTedarikci(ObjectId objectId);
        Result<ICollection<Tedarikci>> getAll();
        Result<Tedarikci> getTedarikciById(ObjectId objectId);

    }
}
