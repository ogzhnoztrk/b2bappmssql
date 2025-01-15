using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface ISatisService
    {
        void addSatis(Satis satis);
        void addManySatis(List<Satis> satislar);
        void updateSatis(Satis satis, string satisId);
        void deleteSatis(  int  objectId);
        Result<ICollection<Satis>> getAll();
        /// <summary>
        /// Dapper ile sorgu yaparak satis tablosundan satis bilgilerini urun ve sube bilgileri ile birlikte getirir.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="urun"></param>
        /// <param name="sube"></param>
        /// <param name="satisTarihi"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<Result<object>> getAllWithUrunAndSubeAsync(
            int offset,
            int limit,
            string sort,
            string order,
            string urun,
            string sube,
            string satisTarihi,
            string filter,
            int year
            );
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
        Result<KarsilastirmaliSatisRapor> getkarsilastirmaliSatisRapor(string tedarikciId, string? firmaId, string? kategoriId, string? subeId, string? urunId, string? donem, DateTime? donem1Tarih1, DateTime? donem1Tarih2);
        Result<SatisDto> getWithUrunAndSube(int objectId);
        Result<Satis> getSatisById(int objectId);

        Task<Result<IEnumerable<int>>> getSatisCountAsync(
            int offset,
            int limit,
            string sort,
            string order,
            string urun,
            string sube,
            string satisTarihi,
            string filter,
            int? year
            );
    }
}
