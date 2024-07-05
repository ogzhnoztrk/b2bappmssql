using B2BApp.Business.Abstract;
using B2BApp.DTOs.FilterDtos;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;
        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        [HttpGet("GetFirmaSubeKategoriAll")]
        public Result<FilterDto> GetFirmaSubeKategoriAll()
        {

            return _filterService.GetFirmaSubeKategoriAll();
        } 

        [HttpGet("GetSubeKategoriFirmaUrunAll")]
        public Result<SubeKategoriFirmaUrunFilter> GetSubeKategoriFirmaUrunAll()
        {
            return _filterService.GetSubeKategoriFirmaUrunAll();
        }


        [HttpGet("GetSubeTedarikciUrunAll")]
        public Result<SubeUrunTedarikciDto> GetSubeTedarikciUrunAll()
        {
            return _filterService.GetSubeTedarikciUrunAll();
        }

        [HttpGet("GetSubeUrunAllByTedarikciId")]
        public Result<SubeUrunDto> GetSubeUrunAllByTedarikciId(string tedarikciId)
        {
            return _filterService.GetSubeUrunAllByTedarikciId(tedarikciId);
        }
                
        [HttpGet("GetSubeUrunAll")]
        public Result<SubeUrunDto> GetSubeUrunAll()
        {
            return _filterService.GetSubeUrunAll();
        }


    }
}
