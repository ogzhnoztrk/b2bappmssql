using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.TryAddSingleton<IUnitOfWork, UnitOfWork>();



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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
