using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pelicula.Data;
using Microsoft.AspNetCore.Authorization;
using Pelicula.Areas.Identity.Data;
using Pelicula.Servicios;
using Pelicula.Models.DB;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PeliculaDBContextConnection");

builder.Services.AddDbContextPool<PeliculaDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PeliculaContext>(op => op.UseSqlServer(connectionString));


builder.Services.AddTransient<PeliculaServices>();


//builder.Services.AddDefaultIdentity<Pelicula.Areas.Identity.Data.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PeliculaDBContext>();
// Add services to the container.
//builder.Services.AddIdentity<IdentityUser, IdentityRole>


//builder.Services.AddIdentity<IdentityUser, IdentityRole>
//    (
//        options => options.SignIn.RequireConfirmedAccount = false
//    )
//    .AddDefaultUI()
//    .AddDefaultTokenProviders()
//    .AddEntityFrameworkStores<PeliculaDBContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>
    (
        options => options.SignIn.RequireConfirmedAccount = false
    )
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<PeliculaDBContext>();


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

// Scaffold-DbContext "Server=localhost; Database=Pelicula; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/DB

// "PeliculaDBContextConnection": "Server=localhost;Database=Pelicula;Trusted_Connection=True;MultipleActiveResultSets=true"


//namespace Pelicula
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {


//        }
//    }
//}

// <div class="input-group mb-3">
//   <span class="input-group-text" id="basic-addon1">@</span>
//   <input type="text" class="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1">
// </div>

// <div class="input-group mb-3">
//   <input type="text" class="form-control" placeholder="Recipient's username" aria-label="Recipient's username" aria-describedby="basic-addon2">
//   <span class="input-group-text" id="basic-addon2">@example.com</span>
// </div>

// <label for="basic-url" class="form-label">Your vanity URL</label>
// <div class="input-group mb-3">
//   <span class="input-group-text" id="basic-addon3">https://example.com/users/</span>
//   <input type="text" class="form-control" id="basic-url" aria-describedby="basic-addon3">
// </div>