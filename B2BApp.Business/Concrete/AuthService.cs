﻿using B2BApp.Business.Abstract;
using B2BApp.Core.Utilities.Helpers.Security;
using B2BApp.Core.Utilities.Helpers.Security.Hashing;
using B2BApp.DataAccess.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace B2BApp.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtModel jwtModel;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtModel> options, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            jwtModel = options.Value;
            _logger = logger;
        }

        public Result<KullaniciKayitDto> Register(KullaniciKayitDto userRegisterDto)
        {
            var isExist = IsUserExist(userRegisterDto.KullaniciAdi);
            if (!isExist) //kullanıcı mevcut ise
            {
                _logger.LogError("Bu kullanıcı adı alınmış");
                return new Result<KullaniciKayitDto> { Data = null, Message = "Bu kullanıcı adı alınmış", StatusCode = 400, Time = DateTime.Now };
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
                _logger.LogInformation("Kullanıcı Eklendi");
                return new Result<KullaniciKayitDto> { Data = null, Message = "Kullanıcı Eklendi", StatusCode = 200, Time = DateTime.Now };
            }
        }

        public Result<Kullanici> Login(KullaniciGirisDto kullaniciGirisDto)
        {
            var kullanici = _unitOfWork.Kullanici.FilterBy(x => x.KullaniciAdi == kullaniciGirisDto.KullaniciAdi).Data?.FirstOrDefault();
            if (kullanici == null)//kullanıcı Mevcur değilse
            {
                _logger.LogError("Kullanıcı Bulunamadı");
                return new Result<Kullanici> { Data = kullanici, Message = "Kullanıcı Bulunamadı", StatusCode = 400, Time = DateTime.Now };
            }
            if (!HashingHelper.VerifyPasswordHash(kullaniciGirisDto.Sifre, kullanici.SifreHash, kullanici.SifreSalt))
            {
                _logger.LogError("Şifre Veya Kullanıcı Adı Yanlış");
                return new Result<Kullanici> { Data = kullanici, Message = "Şifre Veya Kullanıcı Adı Yanlış", StatusCode = 400, Time = DateTime.Now };
            }
            else
            {
                _logger.LogInformation("Giriş Yapıldı");
                return new Result<Kullanici> { Data = kullanici, Message = "Giriş Yapıldi", StatusCode = 200, Time = DateTime.Now };
            }

        }

        public Result<string> GenerateToken(Kullanici kullanici)
        {
            var generateToken = GenerateAccessToken(kullanici.KullaniciAdi, kullanici.TedarikciId);
            var token = new JwtSecurityTokenHandler().WriteToken(generateToken);
            return new Result<string>(200, "Giris Başarılı", token, DateTime.Now);
        }



        #region
        // Generating token based on user information
        private JwtSecurityToken GenerateAccessToken(string kullaniciAdi, string tedarikciId)
        {
            // Create user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, kullaniciAdi),
                new Claim(ClaimTypes.Role, tedarikciId),
            };

            // JWT'nin oluşturulması
            var issuer = jwtModel.Issuer;
            var audience = jwtModel.Audience;
            var key = Encoding.ASCII.GetBytes(jwtModel.Key);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var stringToken = tokenHandler.WriteToken(token);



            return token as JwtSecurityToken;
        }




        /// <summary>
        /// kkullanıcı yoksa false varsa true döner
        /// </summary>
        /// <param name="kullaniciAdi"></param>
        /// <returns></returns>
        private bool IsUserExist(string kullaniciAdi)
        {
            var kullanici = _unitOfWork.Kullanici.FilterBy(x => x.KullaniciAdi == kullaniciAdi).Data.FirstOrDefault();
            if (kullanici != null)
            {
                return false;

            }
            return true;

        }
        #endregion





    }
}
