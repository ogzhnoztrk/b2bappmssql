using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Context;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Castle.Core.Logging;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

namespace B2BApp.Business.Abstract
{
    public class SatisService : ISatisService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SatisService> _logger;

        public SatisService(IUnitOfWork unitOfWork, ILogger<SatisService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public void addManySatis(List<Satis> satislar)
        {
            try
            {
                var batchSize = 200000;
                var totalSatislar = satislar.Count;
                var batchCount = (int)Math.Ceiling((double)totalSatislar / batchSize);
                var urunler = _unitOfWork.Urun.GetAll().Data;
           
                List<Satis> satislarTemp = new List<Satis>();
                for (int i = 0; i < batchCount; i++)
                {
                    var batch = satislar.Skip(i * batchSize).Take(batchSize).ToList();

                    foreach (var satis in batch)
                    {
                        var urun = urunler.FirstOrDefault(u => u.Id == satis.UrunId);
                        if (urun != null)
                        {
                            var toplam = urun.SatisFiyati * satis.SatisMiktari;
                            var satisSon = new Satis
                            {
                                SatisMiktari = satis.SatisMiktari,
                                SatisTarihi = satis.SatisTarihi,
                                SubeId = satis.SubeId,
                                Toplam = (double)toplam,
                                UrunId = satis.UrunId,
                                Id = satis.Id
                            };
                            satislarTemp.Add(satisSon);
                        
                        }
                       
                    }
   
                    _unitOfWork.Satis.AddMany(satislarTemp);
                    satislarTemp.Clear();
                }

             
                

                _logger.LogInformation("Satışlar eklendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satışlar eklenirken hata oluştu");
                throw;
            }
        }

        public void addSatis(Satis satis)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetFirstOrDefault(x=>x.Id == satis.UrunId).Data;

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;
                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
                    UrunId = satis.UrunId,
                    Id = satis.Id

                };

                _unitOfWork.Satis.Add(satisSon);
                _logger.LogInformation("Satış eklendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış eklenirken hata oluştu");
                throw;
            }
        }

        public void deleteSatis(int objectId)
        {
            try
            {
                _unitOfWork.Satis.Remove(_unitOfWork.Satis.GetFirstOrDefault(x=>x.Id == objectId).Data);
                _logger.LogError("Satış silindi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış silinirken hata oluştu");
                throw;
            }
        }

        public Result<ICollection<Satis>> getAll()
        {
            try
            {

                var stopwatch = Stopwatch.StartNew();

                var data = DapperConn.GetData<Satis>(
                    $"SELECT " +
                    //$" Top 100" +
                    $"sats_id as Id," +
                    $" sube_id as SubeId," +
                    $" urun_id as UrunId," +
                    $" sats_miktari as SatisMiktari," +
                    $" sats_tarihi as SatisTarihi," +
                    $" sats_toplam as Toplam" +

                    $" FROM TBL_SATIS"
                ).ToList();
                stopwatch.Stop();

                return new Result<ICollection<Satis>>(200, "Veri Getirildi", data);

                //_logger.LogInformation("Satışlar getirildi");
                //return _unitOfWork.Satis.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satışlar getirilirken hata oluştu");
                throw;
            }
        }

      
        public async Task<Result<object>> getAllWithUrunAndSubeAsync
            (
            int offset,
            int limit,
            string sort,
            string order,
            string urun,
            string sube,
            string satisTarihi,
            string filter,
            int year
            )
        {
            try
            {

                //var filterResult = JsonConvert.DeserializeObject<Filter>(filter);

                var filterString =
                    $" WHERE" +
                    $" sube.sube_adi LIKE '%{sube}%'   " +
                    $" AND urun.urun_adi LIKE '%{urun}%' " +
                    $" AND s.sats_tarihi LIKE '%{satisTarihi}%'" +
                    $" AND s.sats_tarihi >= '{year}-01-01' AND s.sats_tarihi < '{year+1}-01-01'";
                var query = 
                    $"SELECT " +
                    $" s.sats_id AS Id," +
                    $" s.sube_id AS SubeId," +
                    $" s.urun_id AS UrunId," +
                    $" s.sats_miktari AS SatisMiktari," +
                    $" s.sats_tarihi AS SatisTarihi," +
                    $" s.sats_toplam AS Toplam," +
                    $" urun.urun_adi AS Urun," +
                    $" sube.sube_adi As Sube" +
                    $" FROM TBL_SATIS s" +
                    $" LEFT JOIN TBL_URUN_TANIM urun ON urun.urun_id = s.urun_id" +
                    $" LEFT JOIN TBL_SUBE_TANIM sube ON sube.sube_id= s.sube_id " +
                    filterString +
                    $" ORDER BY {sort.ToUpper(CultureInfo.InvariantCulture)} {order.ToUpper()}" +
                    $" OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY";

                var data = await DapperConn.GetDataAsync<SatisTableDto>(
                    query
                );

                var totalcount = await DapperConn.GetDataAsync<int>(
                    $"SELECT " +
                    //$"TOP 100" +
                    $" Count(sats_id)" +
                    $" FROM TBL_SATIS s" +
                     $" LEFT JOIN TBL_URUN_TANIM urun ON urun.urun_id = s.urun_id" +
                    $" LEFT JOIN TBL_SUBE_TANIM sube ON sube.sube_id= s.sube_id " +
                   filterString

                );

                return new Result<object>(200, "Veri Getirildi ", new
                {
                    Satislar = data,
                    TotalCount = totalcount
                });
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Result<ICollection<SatisDto>> getAllWithUrunAndSubeByTedarikciId(string tedarikciId, DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            try
            {
                //kategoriId null ise tüm ürünleri getirir, değilse sadece o ürünleri getirir
                var urunler = kategoriId == null ? _unitOfWork.Urun.GetAll(x => x.TedarikciId.ToString() == tedarikciId).Data
                    : _unitOfWork.Urun.GetAll(x => x.TedarikciId.ToString() == tedarikciId && x.KategoriId.ToString() == kategoriId).Data;

                var satislar = _unitOfWork.Satis.GetAll().Data;

                //subeId null ise tüm şubeleri getirir, değilse sadece o şubeyi getirir   _unitOfWork.Sube.GetFirstOrDefault(x => x.SubeId == Guid.Parse(subeId)).Data
                var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.GetAll(x=>x.Id.ToString() == subeId.ToString()).Data;

                //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
                subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId.ToString() == firmaId).ToList();

                //ilk ve ikinci tarih filtresi isetnmediğinde kullanılır
                ilkTarih = ilkTarih ?? DateTime.MinValue;
                ikinciTarih = ikinciTarih ?? DateTime.MaxValue;

                var satislarDto = (
                    from urun in urunler
                    join satis in satislar on urun.Id equals satis.UrunId
                    join sube in subeler on satis.SubeId equals sube.Id
                    where satis.SatisTarihi >= ilkTarih && satis.SatisTarihi <= ikinciTarih
                    select new SatisDto
                    {
                        Id = satis.Id.ToString(),
                        SatisMiktari = satis.SatisMiktari,
                        SatisTarihi = satis.SatisTarihi,
                        Sube = sube,
                        Toplam = satis.Toplam,
                        Urun = urun

                    }).ToList();

                var result = new Result<ICollection<SatisDto>>
                {
                    Data = satislarDto,
                    Message = "ürün satışı detayları ile getirldi",
                    StatusCode = 200
                };

                _logger.LogInformation("ürün satışı detayları ile getirldi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ürün satışı detayları getirilirken hata oluştu");
                throw;
            }
        }

        /// <summary>
        /// Rapor ekranı
        /// </summary>
        /// <returns></returns>
        public Result<ICollection<SatisDto>> getAllWithUrunAndSube(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            try
            {
                var stopwwatchstart = Stopwatch.StartNew();
                var stopwatch = Stopwatch.StartNew();
               
                // KategoriId'ye göre ürünleri getir
                var urunler = kategoriId == null
                    ? _unitOfWork.Urun.GetAll().Data
                    : _unitOfWork.Urun.GetAll(x => x.KategoriId.ToString() == kategoriId).Data;
                stopwatch.Stop();

                var stopwatch2 = Stopwatch.StartNew();               
                var satislar = _unitOfWork.Satis.GetAll().Data;
               stopwatch2.Stop();

                var stopwatch3 = Stopwatch.StartNew();
                // SubeId'ye göre şubeleri getir
                var subeler = subeId == null
                    ? _unitOfWork.Sube.GetAll().Data
                    : _unitOfWork.Sube.GetAll(x => x.Id.ToString() == subeId).Data;
                stopwatch3.Stop();
                // FirmaId'ye göre şubeleri filtrele
                if (firmaId != null)
                {
                    subeler = subeler.Where(x => x.FirmaId.ToString() == firmaId).ToList();
                }

                // Tarih aralığını ayarla
                var baslangicTarihi = ilkTarih ?? DateTime.MinValue;
                var bitisTarihi = ikinciTarih ?? DateTime.MaxValue;

                // SatisDto listesini oluştur
                var satislarDto = (
                    from urun in urunler
                    join satis in satislar on urun.Id equals satis.UrunId
                    join sube in subeler on satis.SubeId equals sube.Id
                    where satis.SatisTarihi >= baslangicTarihi && satis.SatisTarihi <= bitisTarihi
                    select new SatisDto
                    {
                        Id = satis.Id.ToString(),
                        SatisMiktari = satis.SatisMiktari,
                        SatisTarihi = satis.SatisTarihi,
                        Sube = sube,
                        Toplam = satis.Toplam,
                        Urun = urun
                    }).ToList();

                // Sonucu oluştur ve döndür
                var result = new Result<ICollection<SatisDto>>
                {
                    Data = satislarDto,
                    Message = "Ürün satışı detayları ile getirildi",
                    StatusCode = 200
                };
                stopwwatchstart.Stop();
                _logger.LogInformation("Ürün satışı detayları ile getirildi");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün satışı detayları getirilirken hata oluştu");
                throw;
            }

        }

        public Result<Satis> getSatisById(int objectId)
        {
            try
            {
                _logger.Equals("Satış getirildi");
                return _unitOfWork.Satis.GetFirstOrDefault(x=>x.Id == objectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış getirilirken hata oluştu");
                throw;
            }

        }

        public Result<SatisDto> getWithUrunAndSube(int objectId)
        {
            var satis = _unitOfWork.Satis.GetFirstOrDefault(x=>x.Id == objectId).Data;
            var sube = _unitOfWork.Sube.GetFirstOrDefault(x=>x.Id == satis.SubeId).Data;
            var urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.Id == satis.UrunId).Data;

            var result = new Result<SatisDto>
            {
                Data = new SatisDto { Id = satis.Id.ToString(), Urun = urun, Sube = sube, SatisMiktari = satis.SatisMiktari, SatisTarihi = satis.SatisTarihi, Toplam = satis.Toplam },
                Message = "Satis bilgileri getirildi",
                StatusCode = 200
            };

            _logger.LogInformation("Satış bilgileri getirildi");
            return result;
        }

        public void updateSatis(Satis satis, string satisId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.Id == satis.UrunId).Data;

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;

                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
                    UrunId = satis.UrunId,
                    Id = satis.Id

                };

                _logger.LogInformation("Satış güncellendi");
                _unitOfWork.Satis.Update(satisSon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış güncellenirken hata oluştu");
                throw;
            }
        }

        public Result<ICollection<KarDto>> getSatisKar(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId, string? urunId)
        {
            try
            {
                // Sorguyu IQueryable olarak başlatın
                var satislarQuery = _unitOfWork.Satis.GetAll().Data.AsQueryable();

                // Filtreleri adım adım uygulayın
                if (ilkTarih.HasValue)
                {
                    satislarQuery = satislarQuery.Where(s => s.SatisTarihi >= ilkTarih.Value);
                }

                if (ikinciTarih.HasValue)
                {
                    satislarQuery = satislarQuery.Where(s => s.SatisTarihi <= ikinciTarih.Value);
                }

                if (!string.IsNullOrEmpty(subeId))
                {
                    satislarQuery = satislarQuery.Where(s => s.Id.ToString() == subeId);
                }

                if (!string.IsNullOrEmpty(firmaId))
                {
                    var subeIds = _unitOfWork.Sube.GetAll().Data.Select(s => s.Id).ToList();
                    satislarQuery = satislarQuery.Where(s => subeIds.Contains(s.SubeId));
                }

                if (!string.IsNullOrEmpty(kategoriId))
                {
                    var urunIds = _unitOfWork.Urun.GetAll().Data.Select(u => u.Id).ToList();
                    satislarQuery = satislarQuery.Where(s => urunIds.Contains(s.UrunId));
                }

                if (!string.IsNullOrEmpty(urunId))
                {
                    satislarQuery = satislarQuery.Where(s => s.Id.ToString() == urunId);
                }

                // Sorguyu çalıştırın ve verileri çekin
                var satislar = satislarQuery.ToList();

                // Gerekli veri sözlüklerini oluşturun
                var urunDict = _unitOfWork.Urun.GetAll().Data.ToDictionary(u => u.Id);
                var subeDict = _unitOfWork.Sube.GetAll().Data.ToDictionary(s => s.Id);
                var firmaDict = _unitOfWork.Firma.GetAll().Data.ToDictionary(f => f.Id);

                // Sonuçları gruplandırın ve hesaplayın
                var groupedSatislar = satislar
                    .GroupBy(x => x.UrunId)
                    .Select(x => new KarDto
                    {
                        Urun = urunDict[x.Key],
                        Sube = subeDict[x.First().SubeId],
                        Firma = firmaDict[subeDict[x.First().SubeId].FirmaId],
                        ToplamSatisFiyat = x.Sum(y => y.Toplam),
                        ToplamKar = x.Sum(y => y.Toplam) - x.Sum(y => urunDict[y.UrunId].Fiyat * y.SatisMiktari),
                        ToplamFiyat = x.Sum(y => urunDict[y.UrunId].Fiyat * y.SatisMiktari),
                        ToplamSatisMiktari = x.Sum(y => y.SatisMiktari),
                    })
                    .ToList();
              

                // Sonucu oluşturun ve geri dönün
                var result = new Result<ICollection<KarDto>>
                {
                    Data = groupedSatislar,
                    Message = "Kârlar getirildi",
                    StatusCode = 200
                };

                _logger.LogInformation("Kâr hesaplandı");
               
             
                return result;
            }
            catch (Exception ex)
            {
                GC.Collect();
                _logger.LogError(ex, "Kâr hesaplanırken bir hata oluştu");
                throw;
            }
            finally
            {
                // Bellek temizliği            
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }



        public Result<KarsilastirmaliSatisRapor> getkarsilastirmaliSatisRapor(string tedarikciId, string? firmaId, string? kategoriId, string? subeId, string? urunId, string? donem, DateTime? donem1Tarih1, DateTime? donem1Tarih2)
        {

            donem ??= "aylik"; donem1Tarih1 ??= DateTime.Now; donem1Tarih2 ??= DateTime.Now;

            var donem2Tarih1 = DateTime.Now;
            var donem2Tarih2 = DateTime.Now;

            //var satislar = _unitOfWork.Satis.GetAll().Data;
            ////Burası Optimize edilecek
            //satislar = satislar.Where(x => _unitOfWork.Urun.GetById(x.UrunId).Data.TedarikciId == tedarikciId).ToList();
            //satislar = firmaId == null ? satislar : satislar.Where(x => _unitOfWork.Sube.GetById(x.SubeId).Data.FirmaId == firmaId).ToList();
            //satislar = kategoriId == null ? satislar : satislar.Where(x => _unitOfWork.Urun.GetById(x.UrunId).Data.KategoriId == kategoriId).ToList();
            //satislar = subeId == null ? satislar : satislar.Where(x => x.SubeId == subeId).ToList();
            //satislar = urunId == null ? satislar : satislar.Where(x => x.UrunId == urunId).ToList();
            var urunler = _unitOfWork.Urun.GetAll(x => x.TedarikciId.ToString() == tedarikciId).Data.AsQueryable();
            var satislar = new List<Satis>();
            foreach (var urun in urunler)
            {
                satislar.AddRange(_unitOfWork.Satis.GetAll(x => x.UrunId == urun.Id).Data);
            }
            satislar = firmaId == null ? satislar : satislar.Where(x => _unitOfWork.Sube.GetFirstOrDefault(q=> q.Id == x.SubeId ).Data.FirmaId.ToString() == firmaId).ToList();
            satislar = kategoriId == null ? satislar : satislar.Where(x => _unitOfWork.Urun.GetFirstOrDefault(q=>q.Id == x.UrunId).Data.KategoriId.ToString() == kategoriId).ToList();
            satislar = subeId == null ? satislar : satislar.Where(x => x.SubeId.ToString() == subeId).ToList();
            satislar = urunId == null ? satislar : satislar.Where(x => x.UrunId.ToString() == urunId).ToList();


            if (donem.Contains("aylik"))
            {
                var fark = donem1Tarih2.Value.Subtract(donem1Tarih1.Value).Days;

                donem2Tarih1 = donem1Tarih1.Value.AddDays(-(Math.Abs(fark)) - 1);
                donem2Tarih2 = donem1Tarih1.Value.AddDays(-1);
            }
            else if (donem.Contains("yillik"))
            {
                donem2Tarih1 = donem1Tarih1.Value.AddYears(-1);
                donem2Tarih2 = donem1Tarih2.Value.AddYears(-1);
            }


            var dto = satislar.GroupBy(x => x.UrunId)
                .Select(x => new KarsilastirmaliSatisRaporDto
                {
                    Urun = _unitOfWork.Urun.GetFirstOrDefault(q=>q.Id == x.Key).Data,
                    Donem1Miktar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => y.SatisMiktari),
                    Donem1Tutar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q=>q.Id == y.UrunId).Data.Fiyat * y.SatisMiktari),
                    Donem2Miktar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => y.SatisMiktari),
                    Donem2Tutar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q=>q.Id == y.UrunId).Data.Fiyat * y.SatisMiktari),
                }).ToList();

            var DONEM1 = satislar.Where(x => x.SatisTarihi >= donem1Tarih1 && x.SatisTarihi <= donem1Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q => q.Id == y.UrunId).Data.Fiyat * y.SatisMiktari));
            var DONEM2 = satislar.Where(x => x.SatisTarihi >= donem2Tarih1 && x.SatisTarihi <= donem2Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q => q.Id == y.UrunId).Data.Fiyat * y.SatisMiktari));
            var donemselToplam = new DonemselToplam
            {
                Donem1 = DONEM1,
                Donem2 = DONEM2
            };

            var x = new KarsilastirmaliSatisRapor
            {
                KarsilastirmaliSatisRaporDtos = dto,
                DonemselToplam = donemselToplam,
                Donem1Tarih = $"{donem1Tarih1.Value.ToShortDateString()} - {donem1Tarih2.Value.ToShortDateString()}",
                Donem2Tarih = $"{donem2Tarih1.ToShortDateString()} - {donem2Tarih2.ToShortDateString()}"
            };
            _logger.LogInformation("Karşılaştırmalı satış raporu getirildi");
            return new Result<KarsilastirmaliSatisRapor>
            {
                Data = x,
                Message = "Karşılaştırmalı satış raporu getirildi",
                StatusCode = 200 
            };

        }

        public async Task<Result<IEnumerable<int>>> getSatisCountAsync
            (
            int offset,
            int limit,
            string sort,
            string order,
            string urun,
            string sube,
            string satisTarihi,
            string filter,
            int? year )
        {
            var filterString =
                 $" WHERE" +
                 $" sube.sube_adi LIKE '%{sube}%'   " +
                 $" AND urun.urun_adi LIKE '%{urun}%' " +
                 $" AND s.sats_tarihi LIKE '%{satisTarihi}%'" +
                 $" AND s.sats_tarihi >= '{year}-01-01' AND s.sats_tarihi < '{year + 1}-01-01'";
            var totalcount = await DapperConn.GetDataAsync<int>(
                    $"SELECT " +
                    //$"TOP 100" +
                    $" Count(sats_id)" +
                    $" FROM TBL_SATIS s" +
                     $" LEFT JOIN TBL_URUN_TANIM urun ON urun.urun_id = s.urun_id" +
                    $" LEFT JOIN TBL_SUBE_TANIM sube ON sube.sube_id= s.sube_id " +
                   filterString

                );
            return new Result<IEnumerable<int>>(200, "Veri Getirildi ", totalcount);

        }
    }
}
