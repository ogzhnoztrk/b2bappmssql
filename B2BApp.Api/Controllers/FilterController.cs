using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
