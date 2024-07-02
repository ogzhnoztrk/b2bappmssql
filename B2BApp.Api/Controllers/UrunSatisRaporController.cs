using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]

    public class UrunSatisRaporController : ControllerBase
	{
        private readonly IUrunSatisRaporServisi _urunSatisRaporServisi;
        public UrunSatisRaporController(IUrunSatisRaporServisi urunSatisRaporServisi)
        {
                _urunSatisRaporServisi = urunSatisRaporServisi;
        }
	
		[HttpGet]//kullanıcıar ulaşabilir
		public Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId)
		{
			return _urunSatisRaporServisi.getUrunlerVeAylikSatislarByTedarikciId(tedarikciId);
		}

	}
}
