using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pelicula.Areas.Identity.Data;
using Pelicula.Models.DB;

namespace Pelicula.Data;

public class PeliculaDBContext : IdentityDbContext<ApplicationUser>
//public class PeliculaDBContext : IdentityDbContext
{
    public PeliculaDBContext(DbContextOptions<PeliculaDBContext> options)
        : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //    // Customize the ASP.NET Identity model and override the defaults if needed.
    //    // For example, you can rename the ASP.NET Identity table names and more.
    //    // Add your customizations after calling base.OnModelCreating(builder);
    //}
    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
    //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
    public  DbSet<Comentario> Comentarios { get; set; } = null!;
    public  DbSet<Genero> Generos { get; set; } = null!;
    public  DbSet<Models.DB.Pelicula> PeliDB { get; set; } = null!;
    public  DbSet<PeliculaGenero> PeliculaGeneros { get; set; } = null!;
    public  DbSet<Puntuacion> Puntuacions { get; set; } = null!;
    //public virtual DbSet<AspNetUser> Puntuacions { get; set; } = null!;
}
