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
    public interface IKullaniciService
    {

        void addKullanici(Kullanici kullanici);
        void updateKullanici(Kullanici kullanici, string kullaniciId);
        void deleteKullanici(ObjectId objectId);
        Result<ICollection<Kullanici>> getAll();
        Result<Kullanici> getKullaniciById(ObjectId objectId);

    }
}
