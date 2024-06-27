using B2BApp.DTOs;
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
    public interface ISubeStokService
    {
        void addSubeStok(SubeStok subeStok);
        void updateSubeStok(SubeStok subeStok, string subeStokId);
        void deleteSubeStok(ObjectId objectId);
        Result<ICollection<SubeStok>> getAll();
        Result<ICollection<SubeStokDto>> getAllWithSubeAndUrun();
        Result<ICollection<SubeStokDto>> getAllWithSubeAndUrunByTedarikciId(string tedarikciId);
        Result<SubeStokDto> getWithSubeAndUrun(ObjectId objectId);
        Result<SubeStok> getSubeStokById(ObjectId objectId);
    }
}
