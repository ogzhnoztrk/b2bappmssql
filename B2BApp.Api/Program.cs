using B2BApp.Business.Abstract;
using B2BApp.Business.Concrete;
using B2BApp.Core.Utilities.Helpers.Security;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IFirmaService, FirmaService>();
builder.Services.AddSingleton<IKategoriService, KategoriService>();
builder.Services.AddSingleton<ISatisService, SatisService>();
builder.Services.AddSingleton<ISubeService, SubeService>();
builder.Services.AddSingleton<IUrunService, UrunService>();
builder.Services.AddSingleton<ISubeStokService, SubeStokService>();
builder.Services.AddSingleton<ITedarikciService, TedarikciService>();
builder.Services.AddSingleton<IUrunSatisRaporServisi, UrunSatisRaporServisi>();
builder.Services.AddSingleton<IFilterService, FilterService>();


builder.Services.AddSingleton<IKullaniciService, KullaniciService>();
builder.Services.AddSingleton<IAuthService, AuthService>();


// Tüm originlere izin verin (Sadece geliþtirme için)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("https://localhost:44328");
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   ;
        });
});





//jwt ile ilgili ayarlar
builder.Services.Configure<JwtModel>(o =>
{
    o.Issuer = builder.Configuration["Jwt:Issuer"];
    o.Audience = builder.Configuration["Jwt:Audience"];
    o.Key = builder.Configuration["Jwt:Key"];
});

//JwtBearer token ile ilgili ayarlar
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {   
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//mongodb ile ilgili ayarlar
builder.Services.Configure<MongoSettings>(o =>
{
    o.ConnectionString = builder.Configuration.GetSection("MongoDbConnectionString:ConnectionString").Value!;
    o.DatabaseName = builder.Configuration.GetSection("MongoDbConnectionString:DatabaseName").Value!;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        o =>
        {
            o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(
        o =>
        {
            o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });
}


// Tüm originlere izin veren CORS politikasý
app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
