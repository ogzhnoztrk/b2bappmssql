using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

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

                    _unitOfWork.Satis.InsertMany(satislarTemp);
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
                var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

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

                _unitOfWork.Satis.InsertOne(satisSon);
                _logger.LogInformation("Satış eklendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış eklenirken hata oluştu");
                throw;
            }
        }

        public void deleteSatis(ObjectId objectId)
        {
            try
            {
                _unitOfWork.Satis.DeleteById(objectId.ToString());
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
                        Id = satis.Id,
                        SatisMiktari = satis.SatisMiktari,
                        SatisTarihi = satis.SatisTarihi,
                        Toplam = satis.Toplam,
                        Sube = _unitOfWork.Sube.GetById(satis.SubeId).Data,
                        Urun = _unitOfWork.Urun.GetById(satis.UrunId).Data,

                    };
                    satisDTOs.Add(satisDto);
                }

                var result = new Result<ICollection<SatisDto>> { Data = satisDTOs, Message = "Satışlar getirildi", StatusCode = 200, Time = DateTime.Now };
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
                var urunler = kategoriId == null ? _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data
                    : _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId && x.KategoriId == kategoriId).Data;

                var satislar = _unitOfWork.Satis.GetAll().Data;

                //subeId null ise tüm şubeleri getirir, değilse sadece o şubeyi getirir
                var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;

                //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
                subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId == firmaId).ToList();

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
                        Id = satis.Id,
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
                    StatusCode = 200,
                    Time = DateTime.Now

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
                //kategoriId null ise tüm ürünleri getirir, değilse sadece o ürünleri getirir
                var urunler = kategoriId == null ? _unitOfWork.Urun.GetAll().Data
                    : _unitOfWork.Urun.FilterBy(x => x.KategoriId == kategoriId).Data;

                var satislar = _unitOfWork.Satis.GetAll().Data;


                //subeId null ise tüm şubeleri getirir, değilse sadece o şubeyi getirir
                var subeler = subeId == null ? _unitOfWork.Sube.GetAll().Data : _unitOfWork.Sube.FilterBy(x => x.Id == subeId).Data;

                //firmaId null ise tüm firmaları getirir, değilse sadece o firmayı getirir
                subeler = firmaId == null ? subeler : subeler.Where(x => x.FirmaId == firmaId).ToList();

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
                        Id = satis.Id,
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
                    StatusCode = 200,
                    Time = DateTime.Now

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

        public Result<Satis> getSatisById(ObjectId objectId)
        {
            try
            {
                _logger.Equals("Satış getirildi");
                return _unitOfWork.Satis.GetById(objectId.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış getirilirken hata oluştu");
                throw;
            }

        }

        public Result<SatisDto> getWithUrunAndSube(ObjectId objectId)
        {
            var satis = _unitOfWork.Satis.GetById(objectId.ToString()).Data;
            var sube = _unitOfWork.Sube.GetById(satis.SubeId).Data;
            var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

            var result = new Result<SatisDto>
            {
                Data = new SatisDto { Id = satis.Id, Urun = urun, Sube = sube, SatisMiktari = satis.SatisMiktari, SatisTarihi = satis.SatisTarihi, Toplam = satis.Toplam },
                Message = "Satis bilgileri getirildi",
                StatusCode = 200,
                Time = DateTime.Now,
            };

            _logger.LogInformation("Satış bilgileri getirildi");
            return result;
        }

        public void updateSatis(Satis satis, string satisId)
        {
            try
            {
                var urun = _unitOfWork.Urun.GetById(satis.UrunId).Data;

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
                _unitOfWork.Satis.ReplaceOne(satisSon, satis.Id.ToString());
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
                var satislar = _unitOfWork.Satis.GetAll().Data.AsQueryable();

                if (ilkTarih.HasValue)
                    satislar = satislar.Where(x => x.SatisTarihi >= ilkTarih);

                if (ikinciTarih.HasValue)
                    satislar = satislar.Where(x => x.SatisTarihi <= ikinciTarih);

                if (!string.IsNullOrEmpty(subeId))
                    satislar = satislar.Where(x => x.SubeId == subeId);

                if (!string.IsNullOrEmpty(firmaId))
                {
                    var subeIds = _unitOfWork.Sube.GetAll()
                                                  .Data
                                                  .Where(s => s.FirmaId == firmaId)
                                                  .Select(s => s.Id)
                                                  .ToList();
                    satislar = satislar.Where(x => subeIds.Contains(x.SubeId));
                }

                if (!string.IsNullOrEmpty(kategoriId))
                {
                    var urunIds = _unitOfWork.Urun.GetAll()
                                                  .Data
                                                  .Where(u => u.KategoriId == kategoriId)
                                                  .Select(u => u.Id)
                                                  .ToList();
                    satislar = satislar.Where(x => urunIds.Contains(x.UrunId));
                }

                if (!string.IsNullOrEmpty(urunId))
                    satislar = satislar.Where(x => x.UrunId == urunId);

                var satisList = satislar.ToList();

                var urunDict = _unitOfWork.Urun.GetAll().Data.ToDictionary(u => u.Id);
                var subeDict = _unitOfWork.Sube.GetAll().Data.ToDictionary(s => s.Id);
                var firmaDict = _unitOfWork.Firma.GetAll().Data.ToDictionary(f => f.Id);

                var groupedSatislar = satisList.GroupBy(x => x.UrunId)
                    .Select(x => new KarDto
                    {
                        Urun = urunDict[x.Key],
                        Sube = subeDict[x.First().SubeId],
                        Firma = firmaDict[subeDict[x.First().SubeId].FirmaId],
                        ToplamSatisFiyat = x.Sum(y => y.Toplam),
                        ToplamKar = x.Sum(y => y.Toplam) - x.Sum(y => urunDict[y.UrunId].Fiyat * y.SatisMiktari),
                        ToplamFiyat = x.Sum(y => urunDict[y.UrunId].Fiyat * y.SatisMiktari),
                        ToplamSatisMiktari = x.Sum(y => y.SatisMiktari),
                    }).ToList();

                var result = new Result<ICollection<KarDto>>
                {
                    Data = groupedSatislar,
                    Message = "Kârlar getirildi",
                    StatusCode = 200,
                    Time = DateTime.Now
                };

                _logger.LogInformation("Kâr hesaplandı");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kâr hesaplanırken bir hata oluştu");
                throw;
            }
        }


        public Result<KarsilastirmaliSatisRapor> getkarsilastirmaliSatisRapor(string tedarikciId, string? firmaId, string? kategoriId, string? subeId, string? urunId, string? donem, DateTime? donem1Tarih1, DateTime? donem1Tarih2)
        {

            donem ??= "aylik"; donem1Tarih1 ??= DateTime.Now; donem1Tarih2 ??= DateTime.Now;

            var donem2Tarih1 = DateTime.Now;
            var donem2Tarih2 = DateTime.Now;

            var satislar = _unitOfWork.Satis.GetAll().Data;
            satislar = satislar.Where(x => _unitOfWork.Urun.GetById(x.UrunId).Data.TedarikciId == tedarikciId).ToList();
            satislar = firmaId == null ? satislar : satislar.Where(x => _unitOfWork.Sube.GetById(x.SubeId).Data.FirmaId == firmaId).ToList();
            satislar = kategoriId == null ? satislar : satislar.Where(x => _unitOfWork.Urun.GetById(x.UrunId).Data.KategoriId == kategoriId).ToList();
            satislar = subeId == null ? satislar : satislar.Where(x => x.SubeId == subeId).ToList();
            satislar = urunId == null ? satislar : satislar.Where(x => x.UrunId == urunId).ToList();

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
                    Urun = _unitOfWork.Urun.GetById(x.Key).Data,
                    Donem1Miktar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => y.SatisMiktari),
                    Donem1Tutar = x.Where(y => y.SatisTarihi >= donem1Tarih1 && y.SatisTarihi <= donem1Tarih2).Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari),
                    Donem2Miktar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => y.SatisMiktari),
                    Donem2Tutar = x.Where(y => y.SatisTarihi >= donem2Tarih1 && y.SatisTarihi <= donem2Tarih2).Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari),
                }).ToList();

            var DONEM1 = satislar.Where(x => x.SatisTarihi >= donem1Tarih1 && x.SatisTarihi <= donem1Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari));
            var DONEM2 = satislar.Where(x => x.SatisTarihi >= donem2Tarih1 && x.SatisTarihi <= donem2Tarih2)
                .GroupBy(x => x.SatisTarihi.Date)
                .ToDictionary(x => x.Key, x => x.Sum(y => _unitOfWork.Urun.GetById(y.UrunId).Data.Fiyat * y.SatisMiktari));
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
                StatusCode = 200,
                Time = DateTime.Now
            };

        }

    
    }
}
