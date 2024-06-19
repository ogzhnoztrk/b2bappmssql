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
    public interface ISatisService
    {
        void addSatis(Satis satis);
        void updateSatis(Satis satis);
        void deleteSatis(ObjectId objectId);
        Result<ICollection<Satis>> getAll();
        Result<Satis> getSatisById(ObjectId objectId);
    }
}
