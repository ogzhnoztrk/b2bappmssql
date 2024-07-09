using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;

namespace B2BApp.Business.Abstract
{
    public interface IAuthService
    {
        Result<KullaniciKayitDto> Register(KullaniciKayitDto userRegisterDto);
        Result<Kullanici> Login(KullaniciGirisDto kullaniciGirisDto);
        Result<string> GenerateToken(Kullanici kullanici);

    }
}
