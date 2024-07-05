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
    public interface ISatisService
    {
        void addSatis(Satis satis);
        void updateSatis(Satis satis, string satisId);
        void deleteSatis(ObjectId objectId);
        Result<ICollection<Satis>> getAll();
        Result<ICollection<SatisDto>> getAllWithUrunAndSube();
        Result<ICollection<SatisDto>> getAllWithUrunAndSubeByTedarikciId(
                string tedarikciId,
                DateTime? ilkTarih,
                DateTime? ikinciTarih,
                string? subeId,
                string? kategoriId,
                string? firmaId
            );       
        Result<ICollection<SatisDto>> getAllWithUrunAndSube(
                DateTime? ilkTarih,
                DateTime? ikinciTarih,
                string? subeId,
                string? kategoriId,
                string? firmaId
            );
        Result<ICollection<KarDto>> getSatisKar(
                DateTime? ilkTarih,
                DateTime? ikinciTarih,
                string? subeId,
                string? kategoriId,
                string? firmaId,
                string? urunId
            );

        Result<SatisDto> getWithUrunAndSube(ObjectId objectId);
        Result<Satis> getSatisById(ObjectId objectId);
    }
}
