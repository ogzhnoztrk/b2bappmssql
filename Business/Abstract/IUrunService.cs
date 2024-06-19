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

        void addUrunlerService(UrunService urunlerService);
        void updateUrunlerService(UrunService urunlerService);
        void deleteUrunlerService(ObjectId objectId);
        Result<ICollection<UrunService>> getAll();
        Result<UrunService> getUrunlerServiceById(ObjectId objectId);



    }
}
