using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace B2BApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) 
        { 
            _authService = authService;
        }

        [HttpPost]
        public Result<KullaniciKayitDto> Register(KullaniciKayitDto kullaniciKayitDto)
        {
            return _authService.Register(kullaniciKayitDto);
        }      
        
        [HttpPost("login")]
        public Result<KullaniciGirisDto> login(KullaniciGirisDto kullaniciGirisDto)
        {
            return _authService.Login(kullaniciGirisDto);
        }

    }
}
