using B2BApp.Business.Abstract;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Concrete
{
    public class FilterService : IFilterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FilterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result<FilterDto> GetFirmaSubeKategoriAll()
        {
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
    }
}
