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
    public interface IFirmalarService
    {

        void addFirma(Firma firma);
        Result<ICollection<Firma>> getAll();
        Result<Firma> getFirmaById(ObjectId objectId);

    }
}
