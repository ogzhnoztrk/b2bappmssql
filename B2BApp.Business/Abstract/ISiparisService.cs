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
    public interface ISiparisService
    {

        void addSiparis(Siparis siparis);
        void updateSiparis(Siparis siparis, string siparisId);
        void deleteSiparis(ObjectId objectId);
        Result<ICollection<Siparis>> getAll();
        Result<ICollection<SiparisDto>> getAllWithDetails();
        Result<ICollection<SiparisDto>> getAllWithDetailsByFiltersAndTedarikciId(string tedarikciId, DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi);
        Result<ICollection<SiparisDto>> getAllWithDetailsByFilters(DateTime? tarih1, DateTime? tarih2, string? urunId, string? subeId, bool? aktifMi);
        Result<SiparisDto> getAllWithDetailsById(string siparisId);
        Result<Siparis> getSiparisById(ObjectId objectId);
        Result<String> changeAktiflik(string siparisId);

    }
}
