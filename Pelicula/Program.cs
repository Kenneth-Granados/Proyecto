using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pelicula.Data;
using Microsoft.AspNetCore.Authorization;
using Pelicula.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PeliculaDBContextConnection");
builder.Services.AddDbContext<PeliculaDBContext>(options => options.UseSqlServer(connectionString));//Inyeccion de dependencia

//builder.Services.AddDefaultIdentity<Pelicula.Areas.Identity.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PeliculaDBContext>();
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<PeliculaDBContext>();

builder.Services.AddControllersWithViews();//

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
app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endPoint =>
{
    endPoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endPoint.MapRazorPages();
});

app.Run();
