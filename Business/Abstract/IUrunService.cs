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
    public interface IUrunService
    {

        void addUrun(Urun urunler);
        void updateUrun(Urun urunler, string urunId);
        void deleteUrun(ObjectId objectId);
        Result<ICollection<Urun>> getAll();
        Result<Urun> getUrunById(ObjectId objectId);



    }
}
