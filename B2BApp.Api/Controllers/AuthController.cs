﻿using B2BApp.Business.Abstract;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Net;

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
        public Result<string> login(KullaniciGirisDto kullaniciGirisDto)
        {
            var login = _authService.Login(kullaniciGirisDto);

            if (login.StatusCode == 400) 
            {
                return new Result<string>(200,"Giriş Yapılamadı","Giriş Yapılırken Bir Sorun Oluştu",DateTime.Now);
            }

            //burada token oluşturulacak ve token döndürülecek



            return _authService.GenerateToken(login.Data);
        }

    }
}