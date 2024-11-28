﻿using B2BApp.Core.Models.Concrete;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface IFirmaService
    {

        void addFirma(Firma firma);
        void updateFirma(Firma firma, string firmaId);
        void deleteFirma(ObjectId objectId);
        Result<ICollection<Firma>> getAll();
        Result<Firma> getFirmaById(ObjectId objectId);

    }
}
