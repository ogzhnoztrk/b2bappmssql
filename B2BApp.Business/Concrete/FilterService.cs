using B2BApp.Business.Abstract;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs.FilterDtos;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;

namespace B2BApp.Business.Concrete
{
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FilterService> _logger;
        public FilterService(IUnitOfWork unitOfWork, ILogger<FilterService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public Result<FilterDto> GetFirmaSubeKategoriAll()
        {
            _logger.LogInformation("Firma, Şube ve Kategori bilgileri getiriliyor");
            return new Result<FilterDto>
            {
                Data = new FilterDto
                {
                    Kategoriler = _unitOfWork.Kategori.GetAll().Data,
                    Firmalar = _unitOfWork.Firma.GetAll().Data,
                    Subeler = _unitOfWork.Sube.GetAll().Data
                },
                Message = "Firma, Şube ve Kategori bilgileri başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };
        }

        public Result<SubeUrunTedarikciDto> GetSubeTedarikciUrunAll()
        {
            _logger.Equals("Şube, Tedarikçi ve Ürün bilgileri getiriliyor");
            return new Result<SubeUrunTedarikciDto>
            {
                Data = new SubeUrunTedarikciDto
                {
                    Subeler = _unitOfWork.Sube.GetAll().Data,
                    Tedarikciler = _unitOfWork.Tedarikci.GetAll().Data,
                    Urunler = _unitOfWork.Urun.GetAll().Data,
                },
                Message = "Şube, Tedarikçi ve Ürün bilgileri başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };
        }

        public Result<SubeUrunDto> GetSubeUrunAllByTedarikciId(string tedarikciId)
        {
            _logger.LogInformation("Şube ve Ürün bilgileri getiriliyor");
            return new Result<SubeUrunDto>
            {
                Data = new SubeUrunDto
                {
                    Subeler = _unitOfWork.Sube.GetAll().Data,
                    Urunler = _unitOfWork.Urun.FilterBy(x => x.TedarikciId == tedarikciId).Data
                },
                Message = "Şube ve Ürün bilgileri başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };
        }

        public Result<SubeUrunDto> GetSubeUrunAll()
        {
            _logger.LogInformation("Şube ve Ürün bilgileri getiriliyor");
            return new Result<SubeUrunDto>
            {
                Data = new SubeUrunDto
                {
                    Subeler = _unitOfWork.Sube.GetAll().Data,
                    Urunler = _unitOfWork.Urun.GetAll().Data
                },
                Message = "Şube ve Ürün bilgileri başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };
        }

        public Result<SubeKategoriFirmaUrunFilter> GetSubeKategoriFirmaUrunAll()
        {
            _logger.LogInformation("Firma, Şube ve Kategori bilgileri başarıyla getirildi");
            return new Result<SubeKategoriFirmaUrunFilter>
            {
                Data = new SubeKategoriFirmaUrunFilter
                {
                    Kategoriler = _unitOfWork.Kategori.GetAll().Data,
                    Firmalar = _unitOfWork.Firma.GetAll().Data,
                    Subeler = _unitOfWork.Sube.GetAll().Data,
                    Urunler = _unitOfWork.Urun.GetAll().Data
                },
                Message = "Firma, Şube ve Kategori bilgileri başarıyla getirildi",
                StatusCode = 200,
                Time = DateTime.Now
            };
        }
    }
}
