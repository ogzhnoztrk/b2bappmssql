using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

[Obsolete("Do not use this in Production code!!!", true)]
static void NEVER_EAT_POISON_Disable_CertificateValidation()
{
    // Disabling certificate validation can expose you to a man-in-the-middle attack
    // which may allow your encrypted message to be read by an attacker
    // https://stackoverflow.com/a/14907718/740639
    ServicePointManager.ServerCertificateValidationCallback =
        delegate (
            object s,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        ) {
            return true;
        };
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
