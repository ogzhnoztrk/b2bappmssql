﻿using B2BApp.Core.Models.Concrete;
using B2BApp.DTOs.FilterDtos;

namespace B2BApp.Business.Abstract
{
    public interface IFilterService
    {
        Result<FilterDto> GetFirmaSubeKategoriAll();
        Result<SubeKategoriFirmaUrunFilter> GetSubeKategoriFirmaUrunAll();
        Result<SubeKategoriFirmaUrunFilter> GetSubeKategoriFirmaUrunAllBytedarikciId(string tedarikciId);
        Result<SubeUrunTedarikciDto> GetSubeTedarikciUrunAll();
        Result<SubeUrunDto> GetSubeUrunAllByTedarikciId(string tedarikciId);
        Result<SubeUrunDto> GetSubeUrunAll();

    }
}
