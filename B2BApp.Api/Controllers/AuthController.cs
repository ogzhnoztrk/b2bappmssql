using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        //[Authorize(Roles = "6682972f420b0208d3d620a7")]

        public Result<KullaniciKayitDto> Register(KullaniciKayitDto kullaniciKayitDto)
        {
            return _authService.Register(kullaniciKayitDto);
        }

        [HttpPost("login")]//kullanıcılar ulaşabilir
        public Result<string> login(KullaniciGirisDto kullaniciGirisDto)
        {
            var login = _authService.Login(kullaniciGirisDto);

            if (login.StatusCode == 400)
            {
                return new Result<string>(400, "Giriş Yapılamadı", "Giriş Yapılırken Bir Sorun Oluştu" );
            }

            //burada token oluşturulacak ve token döndürülecek



            return _authService.GenerateToken(login.Data);
        }

    }
}
