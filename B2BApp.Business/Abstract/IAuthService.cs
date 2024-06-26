using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
    public interface IAuthService
    {
        Result<KullaniciKayitDto> Register(KullaniciKayitDto userRegisterDto);
        Result<string> Login(KullaniciGirisDto kullaniciGirisDto);
        
    }
}
