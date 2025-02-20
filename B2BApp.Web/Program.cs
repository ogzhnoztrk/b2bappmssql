using B2BApp.Web.Helpers.HttpHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();








Config.ip = builder.Configuration["Config:ip"];
Config.port = int.Parse(builder.Configuration["Config:port"]);
Config.usessl = bool.Parse(builder.Configuration["Config:usessl"]);
Config.startpath = builder.Configuration["Config:startpath"];

HttpService.ApiLink = HttpService._apiLinkGenerate(Config.ip, Config.port, Config.usessl, Config.startpath);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
