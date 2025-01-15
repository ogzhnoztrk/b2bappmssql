using B2BApp.Business.Abstract;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Context;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace B2BApp.Business.Concrete
{
    public class UrunSatisRaporServisi : IUrunSatisRaporServisi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UrunSatisRaporServisi> _logger;
        public UrunSatisRaporServisi(IUnitOfWork unitOfWork, ILogger<UrunSatisRaporServisi> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Burası
        /// </summary>
        /// <param name="tedarikciId"></param>
        /// <returns></returns>
        public Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId)
        {
            var stopwatchGeneral = Stopwatch.StartNew();

            // Zaman ölçümü için Stopwatch başlat
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var urunler = _unitOfWork.Urun.GetAll(x => x.TedarikciId.ToString() == tedarikciId).Data;
                var query = $"SELECT " +
                    $" satis.sats_id AS Id," +
                    $" satis.sube_id AS SubeId," +
                    $" satis.urun_id AS UrunId," +
                    $" satis.sats_miktari AS SatisMiktari," +
                    $" satis.sats_tarihi AS SatisTarihi," +
                    $" satis.sats_toplam AS Toplam" +
                    $" FROM TBL_SATIS satis" +
                    $" LEFT JOIN TBL_URUN_TANIM urun on satis.urun_id = urun.urun_id" +
                    $" LEFT JOIN TBL_TEDARIKCILER tedarikci on tedarikci.tdrk_id = urun.tdrk_id" +
                    $" where tedarikci.tdrk_id LIKE '%{tedarikciId}%'" +
                    $" and  CAST(satis.sats_tarihi as date) between '{DateTime.Now.Year}-01-01' and '{DateTime.Now.Year}-12-31'";
                var satislar = DapperConn.GetData<Satis>(query);
                stopwatch.Stop();//filtreli 4 saniye


                var subeler = _unitOfWork.Sube.GetAll().Data;
                var stopwatch2 = Stopwatch.StartNew();
                var satislarDto =
                    (
                        from urun in urunler
                        where urun.TedarikciId.ToString() == tedarikciId
                        join satis in satislar on urun.Id equals satis.UrunId
                        join sube in subeler on satis.SubeId equals sube.Id
                        select new SatisDto
                        {
                            Id = satis.Id.ToString(),
                            Sube = sube,
                            Urun = urun,
                            SatisMiktari = satis.SatisMiktari,
                            SatisTarihi = satis.SatisTarihi,
                            Toplam = satis.Toplam
                        }
                    );
                
                var stopwatch3 = Stopwatch.StartNew();
                
                var aylikSatislarByAyAdi = (
                    from satis in satislarDto
                    group satis by satis.SatisTarihi.ToString("MMMM") into g
                    select new { AyAdi = g.Key, Toplam = g.Sum(x => x.Toplam) }
                ).ToDictionary(x => x.AyAdi, x => x.Toplam);
            stopwatch3.Stop();

                var stopwatch4 = Stopwatch.StartNew();
                var toplamUrunSatis = (
                    from urun in urunler
                    where urun.TedarikciId.ToString() == tedarikciId
                    join satis in satislar on urun.Id equals satis.UrunId
                    into g
                    select new { urunAdi = urun.UrunAdi, toplam = g.Sum(x => x.SatisMiktari) }



                    ).ToDictionary(x => x.urunAdi, x => x.toplam);
                stopwatch4.Stop();


                var urunDtos = new List<UrunDto>();

                var stopwatch5 = Stopwatch.StartNew();
                if (urunler != null)
                {
                    foreach (var urun in urunler)
                    {

                        var kategori = _unitOfWork.Kategori.GetFirstOrDefault(x=>x.Id == urun.KategoriId).Data;
                        var tedarikci = _unitOfWork.Tedarikci.GetFirstOrDefault(x => x.Id == urun.TedarikciId).Data;
                        var urunDto = new UrunDto
                        {
                            Kategori = kategori,
                            Fiyat = urun.Fiyat,
                            Id = urun.Id.ToString(),
                            UrunAdi = urun.UrunAdi,
                            Tedarikci = tedarikci

                        };
                        urunDtos.Add(urunDto);
                    }
                }
                

                var result = new Result<UrunlerVeAylikSatislarDto>
                {
                    Data = new UrunlerVeAylikSatislarDto
                    {
                        Urunler = urunDtos,
                        ToplamAySatislar = aylikSatislarByAyAdi,
                        ToplamUrunSatis = toplamUrunSatis
                    },
                    Message = "Ürünler ve Aylık Satışlar Getirildi",
                    StatusCode = 200 

                };
                stopwatchGeneral.Stop();
                stopwatch5.Stop();
                _logger.LogInformation("Ürünler ve Aylık Satışlar Getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürünler ve Aylık Satışlar Getirilirke hata oluştu");
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

    }

}
