using B2BApp.DTOs.FilterDtos;
using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
    public interface IFilterService
    {
        Result<FilterDto> GetFirmaSubeKategoriAll();
        Result<SubeKategoriFirmaUrunFilter> GetSubeKategoriFirmaUrunAll();
        Result<SubeUrunTedarikciDto> GetSubeTedarikciUrunAll();
        Result<SubeUrunDto> GetSubeUrunAllByTedarikciId(string tedarikciId);
        Result<SubeUrunDto> GetSubeUrunAll();

    }
}
