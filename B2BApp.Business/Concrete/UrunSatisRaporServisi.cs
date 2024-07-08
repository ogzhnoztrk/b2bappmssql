using Amazon.Runtime.Internal.Util;
using B2BApp.Business.Abstract;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId)
		{
			try
			{
                var urunler = _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data;
                var satislar = _unitOfWork.Satis.GetAll().Data;
                var subeler = _unitOfWork.Sube.GetAll().Data;

                var satislarDto =
                    (
                        from urun in urunler
                        where urun.TedarikciId == tedarikciId
                        join satis in satislar on urun.Id equals satis.UrunId
                        join sube in subeler on satis.SubeId equals sube.Id
                        select new SatisDto
                        {
                            Id = satis.Id,
                            Sube = sube,
                            Urun = urun,
                            SatisMiktari = satis.SatisMiktari,
                            SatisTarihi = satis.SatisTarihi,
                            Toplam = satis.Toplam
                        }
                    );

                var aylikSatislarByAyAdi = (
                    from satis in satislarDto
                    group satis by satis.SatisTarihi.ToString("MMMM") into g
                    select new { AyAdi = g.Key, Toplam = g.Sum(x => x.Toplam) }
                ).ToDictionary(x => x.AyAdi, x => x.Toplam);


                var toplamUrunSatis = (
                    from urun in urunler
                    where urun.TedarikciId == tedarikciId
                    join satis in satislar on urun.Id equals satis.UrunId
                    into g
                    select new { urunAdi = urun.UrunAdi, toplam = g.Sum(x => x.SatisMiktari) }



                    ).ToDictionary(x => x.urunAdi, x => x.toplam);


                var urunDtos = new List<UrunDto>();

                if (urunler != null)
                {
                    foreach (var urun in urunler)
                    {

                        var kategori = _unitOfWork.Kategori.GetById(urun.KategoriId).Data;
                        var tedarikci = _unitOfWork.Tedarikci.GetById(urun.TedarikciId).Data;
                        var urunDto = new UrunDto
                        {
                            Kategori = kategori,
                            Fiyat = urun.Fiyat,
                            Id = urun.Id,
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
                    StatusCode = 200,
                    Time = DateTime.Now

                };



                _logger.LogInformation("Ürünler ve Aylık Satışlar Getirildi");
                return result;
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, "Ürünler ve Aylık Satışlar Getirilirke hata oluştu");
				throw;
			}
		}
	
	}
	
}
