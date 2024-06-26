using B2BApp.Business.Abstract;
using B2BApp.Core.Utilities.Helpers.Security;
using B2BApp.Core.Utilities.Helpers.Security.Hashing;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtModel jwtModel;

        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtModel> options)
        {
            _unitOfWork = unitOfWork;
           jwtModel = options.Value;
        }

        public Result<KullaniciKayitDto> Register(KullaniciKayitDto userRegisterDto)
        {
            var isExist = IsUserExist(userRegisterDto.KullaniciAdi);
            if (isExist) //kullanıcı mevcut ise
            {
                return new Result<KullaniciKayitDto> { Data = null, Message = "Bu kullanıcı adı alınmış", StatusCode = 200, Time = DateTime.Now };
            }
            else
            {
                byte[] passwordHash, passwordSalt;
                //şifreyi hashleme
                HashingHelper.CreatePasswordHash(userRegisterDto.Sifre, out passwordHash, out passwordSalt);
                Kullanici kullanici = new Kullanici
                {
                    KullaniciAdi = userRegisterDto.KullaniciAdi,
                    SifreHash = passwordHash,
                    SifreSalt = passwordSalt,
                    TedarikciId = userRegisterDto.TedarikciId
                };
                _unitOfWork.Kullanici.InsertOne(kullanici);
                return new Result<KullaniciKayitDto> { Data = null, Message = "Kullanıcı Eklendi", StatusCode = 200, Time = DateTime.Now };
            }
        }

        public Result<KullaniciGirisDto> Login(KullaniciGirisDto kullaniciGirisDto)
        {
            var kullanici = _unitOfWork.Kullanici.FilterBy(x => x.KullaniciAdi == kullaniciGirisDto.KullaniciAdi).Data.First();
            if (kullanici == null)//kullanıcı Mevcur değilse
            {
                return new Result<KullaniciGirisDto> { Data = null, Message = "Kullanıcı Bulunamadı", StatusCode = 200, Time = DateTime.Now };
            }
            if (!HashingHelper.VerifyPasswordHash(kullaniciGirisDto.Sifre, kullanici.SifreHash, kullanici.SifreSalt))
            {
                return new Result<KullaniciGirisDto> { Data = null, Message = "Şifre Veya Kullanıcı Adı Yanlış", StatusCode = 200, Time = DateTime.Now };
            }
            else
            {
                return new Result<KullaniciGirisDto> { Data = null, Message = "Giriş Yapıldi", StatusCode = 200, Time = DateTime.Now };
            }

        }



      
        /// <summary>
        /// kkullanıcı yoksa false varsa true döner
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <returns></returns>
        private bool IsUserExist(string kullaniciAdi)
        {
            var kullanici = _unitOfWork.Kullanici.FilterBy(x => x.KullaniciAdi == kullaniciAdi);
            if (kullanici != null)
            {
                return false;

            }
            return true;

        }

    }
}
