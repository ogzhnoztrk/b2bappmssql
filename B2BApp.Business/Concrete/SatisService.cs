using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Castle.Core.Logging;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
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
                        var urun = urunler.FirstOrDefault(u => u.UrunId == satis.UrunId);
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
                                SatisId = satis.SatisId
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
                var urun = _unitOfWork.Urun.GetFirstOrDefault(x=>x.UrunId == satis.UrunId).Data;

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;
                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
                    UrunId = satis.UrunId,
                    SatisId = satis.SatisId

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
                _unitOfWork.Satis.Remove(_unitOfWork.Satis.GetFirstOrDefault(x=>x.SatisId == objectId).Data);
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
                _logger.LogInformation("Satışlar getirildi");
                return _unitOfWork.Satis.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satışlar getirilirken hata oluştu");
                throw;
            }
        }

        public Result<ICollection<SatisDto>> getAllWithUrunAndSube()
        {
            try
            {
                var satislar = _unitOfWork.Satis.GetAll().Data;

                var satisDTOs = new List<SatisDto>();

                foreach (var satis in satislar)
                {
                    
                    var satisDto = new SatisDto
                    {
                        Id = satis.SatisId.ToString(),
                        SatisMiktari = satis.SatisMiktari,
                        SatisTarihi = satis.SatisTarihi,
                        Toplam = satis.Toplam,
                        Sube = _unitOfWork.Sube.GetFirstOrDefault(x => x.SubeId == satis.SubeId).Data,
                        Urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.UrunId == satis.UrunId).Data,

                    };
                    satisDTOs.Add(satisDto);
                }

                var result = new Result<ICollection<SatisDto>> { Data = satisDTOs, Message = "Satışlar getirildi", StatusCode = 200 };
                _logger.LogInformation("Satışlar getirildi");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satışlar getirilirken hata oluştu");
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
                var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.GetAll(x=>x.SubeId.ToString() == subeId.ToString()).Data;

                //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
                subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId.ToString() == firmaId).ToList();

                //ilk ve ikinci tarih filtresi isetnmediğinde kullanılır
                ilkTarih = ilkTarih ?? DateTime.MinValue;
                ikinciTarih = ikinciTarih ?? DateTime.MaxValue;

                var satislarDto = (
                    from urun in urunler
                    join satis in satislar on urun.UrunId equals satis.UrunId
                    join sube in subeler on satis.SubeId equals sube.SubeId
                    where satis.SatisTarihi >= ilkTarih && satis.SatisTarihi <= ikinciTarih
                    select new SatisDto
                    {
                        Id = satis.SatisId.ToString(),
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

        public Result<ICollection<SatisDto>> getAllWithUrunAndSube(DateTime? ilkTarih, DateTime? ikinciTarih, string? subeId, string? kategoriId, string? firmaId)
        {
            try
            {
                // KategoriId'ye göre ürünleri getir
                var urunler = kategoriId == null
                    ? _unitOfWork.Urun.GetAll().Data
                    : _unitOfWork.Urun.GetAll(x => x.KategoriId.ToString() == kategoriId).Data;

                var satislar = _unitOfWork.Satis.GetAll().Data;

                // SubeId'ye göre şubeleri getir
                var subeler = subeId == null
                    ? _unitOfWork.Sube.GetAll().Data
                    : _unitOfWork.Sube.GetAll(x => x.SubeId.ToString() == subeId).Data;

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
                    join satis in satislar on urun.UrunId equals satis.UrunId
                    join sube in subeler on satis.SubeId equals sube.SubeId
                    where satis.SatisTarihi >= baslangicTarihi && satis.SatisTarihi <= bitisTarihi
                    select new SatisDto
                    {
                        Id = satis.SatisId.ToString(),
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
                return _unitOfWork.Satis.GetFirstOrDefault(x=>x.SatisId == objectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış getirilirken hata oluştu");
                throw;
            }

        }

        public Result<SatisDto> getWithUrunAndSube(int objectId)
        {
            var satis = _unitOfWork.Satis.GetFirstOrDefault(x=>x.SatisId == objectId).Data;
            var sube = _unitOfWork.Sube.GetFirstOrDefault(x=>x.SubeId == satis.SubeId).Data;
            var urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.UrunId == satis.UrunId).Data;

            var result = new Result<SatisDto>
            {
                Data = new SatisDto { Id = satis.SatisId.ToString(), Urun = urun, Sube = sube, SatisMiktari = satis.SatisMiktari, SatisTarihi = satis.SatisTarihi, Toplam = satis.Toplam },
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
                var urun = _unitOfWork.Urun.GetFirstOrDefault(x => x.UrunId == satis.UrunId).Data;

                //var toplam = urun.Fiyat * satis.SatisMiktari;
                var toplam = urun.SatisFiyati * satis.SatisMiktari;

                var satisSon = new Satis
                {
                    SatisMiktari = satis.SatisMiktari,
                    SatisTarihi = satis.SatisTarihi,
                    SubeId = satis.SubeId,
                    Toplam = (double)toplam,
                    UrunId = satis.UrunId,
                    SatisId = satis.SatisId

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
                    satislarQuery = satislarQuery.Where(s => s.SatisId.ToString() == subeId);
                }

                if (!string.IsNullOrEmpty(firmaId))
                {
                    var subeIds = _unitOfWork.Sube.GetAll().Data.Select(s => s.SubeId).ToList();
                    satislarQuery = satislarQuery.Where(s => subeIds.Contains(s.SubeId));
                }

                if (!string.IsNullOrEmpty(kategoriId))
                {
                    var urunIds = _unitOfWork.Urun.GetAll().Data.Select(u => u.UrunId).ToList();
                    satislarQuery = satislarQuery.Where(s => urunIds.Contains(s.UrunId));
                }

                if (!string.IsNullOrEmpty(urunId))
                {
                    satislarQuery = satislarQuery.Where(s => s.SatisId.ToString() == urunId);
                }

                // Sorguyu çalıştırın ve verileri çekin
                var satislar = satislarQuery.ToList();

                // Gerekli veri sözlüklerini oluşturun
                var urunDict = _unitOfWork.Urun.GetAll().Data.ToDictionary(u => u.UrunId);
                var subeDict = _unitOfWork.Sube.GetAll().Data.ToDictionary(s => s.SubeId);
                var firmaDict = _unitOfWork.Firma.GetAll().Data.ToDictionary(f => f.FirmaId);

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
                satislar.AddRange(_unitOfWork.Satis.GetAll(x => x.UrunId == urun.UrunId).Data);
            }
            satislar = firmaId == null ? satislar : satislar.Where(x => _unitOfWork.Sube.GetFirstOrDefault(q=> q.SubeId  == x.SubeId ).Data.FirmaId.ToString() == firmaId).ToList();
            satislar = kategoriId == null ? satislar : satislar.Where(x => _unitOfWork.Urun.GetFirstOrDefault(q=>q.UrunId == x.UrunId).Data.KategoriId.ToString() == kategoriId).ToList();
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
                    Urun = _unitOfWork.Urun.GetFirstOrDefault(q=>q.UrunId == x.Key).Data,
                    Donem1Miktar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => y.SatisMiktari),
                    Donem1Tutar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q=>q.UrunId == y.UrunId).Data.Fiyat * y.SatisMiktari),
                    Donem2Miktar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => y.SatisMiktari),
                    Donem2Tutar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q=>q.UrunId == y.UrunId).Data.Fiyat * y.SatisMiktari),
                }).ToList();

            var DONEM1 = satislar.Where(x => x.SatisTarihi >= donem1Tarih1 && x.SatisTarihi <= donem1Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q => q.UrunId == y.UrunId).Data.Fiyat * y.SatisMiktari));
            var DONEM2 = satislar.Where(x => x.SatisTarihi >= donem2Tarih1 && x.SatisTarihi <= donem2Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetFirstOrDefault(q => q.UrunId == y.UrunId).Data.Fiyat * y.SatisMiktari));
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

       




    }
}
